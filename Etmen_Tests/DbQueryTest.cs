using Xunit;
using Microsoft.EntityFrameworkCore;
using Etmen_DAL.DbContext;
using Etmen_Domain.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Etmen_Tests
{
    public class DbQueryTest
    {
        private const string ConnectionString = "Server=db53939.public.databaseasp.net; Database=db53939; User Id=db53939; Password=s=3T6iA+@E8t; Encrypt=False; MultipleActiveResultSets=True;";

        [Fact]
        public async Task QueryStaffAndProviders()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EtmenDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            using var context = new EtmenDbContext(optionsBuilder.Options);

            var staffProfiles = await context.StaffProfiles
                .Include(sp => sp.HealthcareProvider)
                .Include(sp => sp.ApplicationUser)
                .ToListAsync();

            var users = await context.Users.ToListAsync();

            var outputFilePath = @"C:\Users\Kozmo0_2\.gemini\antigravity\scratch\db_query_output.txt";
            using var writer = new StreamWriter(outputFilePath);

            await writer.WriteLineAsync("=== STAFF PROFILES ===");
            foreach (var sp in staffProfiles)
            {
                await writer.WriteLineAsync($"Id: {sp.Id}");
                await writer.WriteLineAsync($"  UserId: '{sp.ApplicationUserId}'");
                await writer.WriteLineAsync($"  User Email: '{sp.ApplicationUser?.Email ?? "NULL"}'");
                await writer.WriteLineAsync($"  ProviderId: {sp.HealthcareProviderId}");
                await writer.WriteLineAsync($"  Provider Name: '{sp.HealthcareProvider?.Name ?? "NULL"}'");
                await writer.WriteLineAsync($"  Role: {sp.RoleType}, Shift: {sp.ActiveShift}");
                await writer.WriteLineAsync($"  IsInvitationAccepted: {sp.IsInvitationAccepted}");
                await writer.WriteLineAsync($"  InvitationToken: '{sp.InvitationToken}'");
                await writer.WriteLineAsync($"  JoinedAt: {sp.JoinedAt}");
                await writer.WriteLineAsync("--------------------------------------");
            }

            await writer.WriteLineAsync("\n=== ALL HOSPITAL STAFF USERS ===");
            var staffRoles = await context.UserRoles.ToListAsync();
            var roles = await context.Roles.ToListAsync();
            var staffRoleId = roles.FirstOrDefault(r => r.Name == "HospitalStaff")?.Id;

            foreach (var user in users)
            {
                var isStaff = staffRoleId != null && staffRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == staffRoleId);
                if (isStaff)
                {
                    await writer.WriteLineAsync($"User Id: {user.Id}, Email: '{user.Email}', Name: '{user.FirstName} {user.LastName}'");
                }
            }

            await writer.WriteLineAsync("\n=== HEALTHCARE PROVIDERS ===");
            var providers = await context.HealthcareProviders.ToListAsync();
            foreach (var p in providers)
            {
                await writer.WriteLineAsync($"Id: {p.Id}, Name: '{p.Name}', Type: '{p.Type}', Address: '{p.Address}', IsEmergencyCenter: {p.IsEmergencyCenter}, Latitude: {p.Latitude}, Longitude: {p.Longitude}");
            }
        }
    }
}
