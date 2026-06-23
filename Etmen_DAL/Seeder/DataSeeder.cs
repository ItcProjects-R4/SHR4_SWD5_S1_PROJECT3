using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Etmen_DAL.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EtmenDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1️⃣ Seed Roles
            string[] roleNames = { "Patient", "Doctor", "Admin", "CrisisAdmin", "HospitalStaff" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Clean up old dynamic seed data first to ensure fresh and clean database
            {
                await context.AvailableSlots.ExecuteDeleteAsync();
                await context.Appointments.ExecuteDeleteAsync();
                await context.DoctorProviders.ExecuteDeleteAsync();
                await context.Reviews.ExecuteDeleteAsync();
                await context.EmergencyRequests.ExecuteDeleteAsync();
                await context.RiskAssessments.ExecuteDeleteAsync();
                await context.DoctorProfiles.ExecuteDeleteAsync();
                await context.PatientProfiles.ExecuteDeleteAsync();
                await context.HealthcareProviders.ExecuteDeleteAsync();
                await context.StaffProfiles.ExecuteDeleteAsync();
                await context.OutbreakZones.ExecuteDeleteAsync();

                // Delete seeded users starting with doctor/patient/staff/admin to avoid conflicts
                var oldUsersQuery = context.Users.Where(u => u.Email != null && 
                    (u.Email.StartsWith("doctor") || u.Email.StartsWith("patient") || u.Email.StartsWith("staff") || u.Email.StartsWith("admin")) && 
                    u.Email.EndsWith("@etmen.com"));
                var oldUserIdsQuery = oldUsersQuery.Select(u => u.Id);

                await context.Notifications.Where(n => oldUserIdsQuery.Contains(n.UserId)).ExecuteDeleteAsync();
                await context.UserRoles.Where(ur => oldUserIdsQuery.Contains(ur.UserId)).ExecuteDeleteAsync();
                await oldUsersQuery.ExecuteDeleteAsync();
            }

            // 2️⃣ Seed Admin User
            var adminEmail = "admin@etmen.com";
            ApplicationUser adminUser;
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "CrisisAdmin" });
                }
            }

            // 3️⃣ Seed Hospital Staff User
            var staffEmail = "staff@etmen.com";
            ApplicationUser staffUser = null;
            if (await userManager.FindByEmailAsync(staffEmail) == null)
            {
                staffUser = new ApplicationUser
                {
                    UserName = staffEmail,
                    Email = staffEmail,
                    FirstName = "Hospital",
                    LastName = "Staff",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(staffUser, "Staff@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, "HospitalStaff");
                }
            }
            else
            {
                staffUser = await userManager.FindByEmailAsync(staffEmail);
            }

            // 4️⃣ Seed Doctor User
            var doctorEmail = "doctor1@etmen.com";
            ApplicationUser doctorUser = null;
            if (await userManager.FindByEmailAsync(doctorEmail) == null)
            {
                doctorUser = new ApplicationUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail,
                    FirstName = "حازم",
                    LastName = "الببلاوي",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(doctorUser, "Doctor@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctorUser, "Doctor");
                }
            }
            else
            {
                doctorUser = await userManager.FindByEmailAsync(doctorEmail);
            }

            // 5️⃣ Seed Patient User
            var patientEmail = "patient1@etmen.com";
            ApplicationUser patientUser = null;
            if (await userManager.FindByEmailAsync(patientEmail) == null)
            {
                patientUser = new ApplicationUser
                {
                    UserName = patientEmail,
                    Email = patientEmail,
                    FirstName = "أحمد",
                    LastName = "محمد علي",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(patientUser, "Patient@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(patientUser, "Patient");
                }
            }
            else
            {
                patientUser = await userManager.FindByEmailAsync(patientEmail);
            }

            // 6️⃣ Seed Exactly ONE default Hospital and Clinic
            var defaultHospital = new HealthcareProvider
            {
                Name = "مستشفى طوارئ السلام",
                Type = "Hospital",
                Latitude = 30.0444m,
                Longitude = 31.2357m,
                Address = "القاهرة - وسط المدينة",
                Phone = "01012345678",
                AvailableBeds = 50,
                BedCapacity = 100,
                AmbulanceCapacity = 5,
                AvailableAmbulances = 5,
                IsEmergencyCenter = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var defaultClinic = new HealthcareProvider
            {
                Name = "عيادة الرحمة الطبية",
                Type = "Clinic",
                Latitude = 30.0500m,
                Longitude = 31.2400m,
                Address = "القاهرة - الحي السكني",
                Phone = "01098765432",
                IsEmergencyCenter = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await context.HealthcareProviders.AddAsync(defaultHospital);
            await context.HealthcareProviders.AddAsync(defaultClinic);
            await context.SaveChangesAsync();

            // 7️⃣ Link staff@etmen.com to the Hospital via StaffProfile
            if (staffUser != null)
            {
                var staffProfile = new StaffProfile
                {
                    ApplicationUserId = staffUser.Id,
                    HealthcareProviderId = defaultHospital.Id,
                    RoleType = StaffRoleType.Receptionist,
                    ActiveShift = StaffShiftType.Morning,
                    IsInvitationAccepted = true,
                    JoinedAt = DateTime.UtcNow
                };
                context.StaffProfiles.Add(staffProfile);
            }

            // 8️⃣ Link Doctor to DoctorProfile and DoctorProvider
            if (doctorUser != null)
            {
                var doctorProfile = new DoctorProfile
                {
                    ApplicationUserId = doctorUser.Id,
                    FullName = "د. حازم الببلاوي",
                    Specialization = "طوارئ وعناية مركزة",
                    LicenseNumber = "LIC-12345",
                    YearsOfExperience = 12,
                    Bio = "استشاري طب الطوارئ والحالات الحرجة.",
                    ConsultationFee = 200m,
                    IsAvailable = true,
                    IsOnboarded = true,
                    CreatedAt = DateTime.UtcNow
                };
                context.DoctorProfiles.Add(doctorProfile);
                await context.SaveChangesAsync(); // save to generate doctorProfile.Id

                var doctorProvider = new DoctorProvider
                {
                    DoctorProfileId = doctorProfile.Id,
                    HealthcareProviderId = defaultHospital.Id,
                    IsEmergencyDoctor = true,
                    AffiliationRole = "Consultant"
                };

                var doctorProvider2 = new DoctorProvider
                {
                    DoctorProfileId = doctorProfile.Id,
                    HealthcareProviderId = defaultClinic.Id,
                    IsEmergencyDoctor = false,
                    AffiliationRole = "Clinic Doctor"
                };

                context.DoctorProviders.Add(doctorProvider);
                context.DoctorProviders.Add(doctorProvider2);
            }

            // 9️⃣ Link Patient to PatientProfile
            if (patientUser != null)
            {
                var patientProfile = new PatientProfile
                {
                    ApplicationUserId = patientUser.Id,
                    FullName = "أحمد محمد علي",
                    DateOfBirth = DateTime.UtcNow.AddYears(-30),
                    Gender = "Male",
                    Height = 175m,
                    Weight = 70m,
                    ActivityLevel = PhysicalActivityLevel.Moderate,
                    BloodType = "A+",
                    HasChronicDiseases = false,
                    Latitude = 30.0450m,
                    Longitude = 31.2360m,
                    CreatedAt = DateTime.UtcNow
                };
                context.PatientProfiles.Add(patientProfile);
            }

            await context.SaveChangesAsync();

            // 🔟 Seed Default Crisis Configuration
            if (!await context.CrisisConfigurations.AnyAsync())
            {
                context.CrisisConfigurations.Add(new CrisisConfiguration
                {
                    CrisisName = "Default Mode",
                    CrisisType = CrisisType.Viral,
                    SystemMode = SystemMode.Normal,
                    IsActive = false,
                    StartDate = DateTime.UtcNow,
                    EmergencyThreshold = 0.7m,
                    HighRiskThreshold = 0.5m,
                    MediumRiskThreshold = 0.3m
                });
                await context.SaveChangesAsync();
            }

            // 11 Seed Hantavirus Crisis Configuration
            if (!await context.CrisisConfigurations.AnyAsync(c => c.CrisisName.Contains("هانتا") || c.CrisisName.Contains("Hanta")))
            {
                var hanta = new CrisisConfiguration
                {
                    CrisisName = "وباء فيروس هانتا الرئوي (Hantavirus)",
                    CrisisType = CrisisType.Viral,
                    SystemMode = SystemMode.Crisis,
                    IsActive = true,
                    Description = "متلازمة الرئة بفيروس هانتا (Hantavirus Pulmonary Syndrome) - مرض فيروسي حاد ينتقل عن طريق مخلفات القوارض ويسبب فشلاً تنفسياً حاداً وضيق تنفس حاد.",
                    StartDate = DateTime.UtcNow,
                    EmergencyThreshold = 0.75m,
                    HighRiskThreshold = 0.55m,
                    MediumRiskThreshold = 0.35m,
                    SymptomWeights = new List<SymptomWeight>
                    {
                        new SymptomWeight { SymptomName = "ضيق تنفس حاد", Weight = 0.90m, IsEmergencySymptom = true },
                        new SymptomWeight { SymptomName = "حمى شديدة (ارتفاع الحرارة)", Weight = 0.85m, IsEmergencySymptom = true },
                        new SymptomWeight { SymptomName = "سعال جاف", Weight = 0.70m, IsEmergencySymptom = false },
                        new SymptomWeight { SymptomName = "آلام حادة بالعضلات", Weight = 0.60m, IsEmergencySymptom = false },
                        new SymptomWeight { SymptomName = "إرهاق شديد وصداع", Weight = 0.50m, IsEmergencySymptom = false },
                        new SymptomWeight { SymptomName = "غثيان أو قيء", Weight = 0.45m, IsEmergencySymptom = false }
                    }
                };

                // Deactivate other active configurations
                var activeCrises = await context.CrisisConfigurations.Where(c => c.IsActive).ToListAsync();
                foreach (var active in activeCrises)
                {
                    active.IsActive = false;
                }

                context.CrisisConfigurations.Add(hanta);
                await context.SaveChangesAsync();
            }

            // 12 Seed Outbreak Zones
            if (!await context.OutbreakZones.AnyAsync())
            {
                var defaultCrisis = await context.CrisisConfigurations.FirstOrDefaultAsync(c => c.IsActive);
                if (defaultCrisis != null)
                {
                    var outbreakZones = new List<OutbreakZone>
                    {
                        new OutbreakZone
                        {
                            CrisisConfigurationId = defaultCrisis.Id,
                            ZoneName = "بؤرة القاهرة الكبرى",
                            CenterLatitude = 30.0500m,
                            CenterLongitude = 31.2500m,
                            RadiusInKm = 15.0m,
                            RiskLevel = 3, // Emergency
                            CreatedAt = DateTime.UtcNow
                        }
                    };
                    await context.OutbreakZones.AddRangeAsync(outbreakZones);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
