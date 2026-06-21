using Xunit;
using Moq;
using Etmen_BLL.Repositories.Services;
using Etmen_DAL.Repositories.Interfaces;
using Etmen_Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etmen_Tests
{
    public class CrisisRiskEngineTests
    {
        [Fact]
        public async Task CalculateOutbreakProbabilityAsync_AtCenterOfZone_ReturnsCorrectRiskRatio()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var outbreakRepoMock = new Mock<IOutbreakZoneRepository>();

            var activeOutbreakZone = new OutbreakZone
            {
                Id = 1,
                ZoneName = "Test Outbreak Zone",
                CenterLatitude = 30.0444m,  // Cairo Center
                CenterLongitude = 31.2357m,
                RadiusInKm = 10m,
                RiskLevel = 8              // Risk level 8 out of 10
            };

            outbreakRepoMock
                .Setup(r => r.GetByCrisisIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<OutbreakZone> { activeOutbreakZone });

            uowMock
                .Setup(u => u.OutbreakZones)
                .Returns(outbreakRepoMock.Object);

            var service = new CrisisRiskEngineService(uowMock.Object);

            // Act: Calculate probability directly at the center of the outbreak zone
            var result = await service.CalculateOutbreakProbabilityAsync(30.0444m, 31.2357m, 1);

            // Assert
            Assert.True(result.IsSuccess);
            // Distance = 0 km, so proximity ratio = 1.0. 
            // Risk level multiplier = 8 / 10 = 0.8.
            // Expected probability = 1.0 * 0.8 = 0.8
            Assert.Equal(0.8m, result.Data);
        }

        [Fact]
        public async Task CalculateOutbreakProbabilityAsync_OutsideRadius_ReturnsZeroProbability()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var outbreakRepoMock = new Mock<IOutbreakZoneRepository>();

            var activeOutbreakZone = new OutbreakZone
            {
                Id = 1,
                ZoneName = "Test Outbreak Zone",
                CenterLatitude = 30.0444m,
                CenterLongitude = 31.2357m,
                RadiusInKm = 10m,
                RiskLevel = 8
            };

            outbreakRepoMock
                .Setup(r => r.GetByCrisisIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<OutbreakZone> { activeOutbreakZone });

            uowMock
                .Setup(u => u.OutbreakZones)
                .Returns(outbreakRepoMock.Object);

            var service = new CrisisRiskEngineService(uowMock.Object);

            // Act: Calculate probability far outside the zone (e.g. 50 km away)
            var result = await service.CalculateOutbreakProbabilityAsync(30.5m, 31.5m, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(0.0m, result.Data);
        }
    }
}
