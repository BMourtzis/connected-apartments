using ConnApsDomain;
using System;
using Xunit;

namespace CommApsDomainUnitTest.Domain_Tests.Person_Tests
{
    public class CreateBuildingTests
    {
        private Facade cad;

        public CreateBuildingTests()
        {
            cad = new Facade();
        }

        [Fact]
        public void CreatePerson_NormalValues_RegularReturn()
        {
            //Arrange

            //Act
            var building = cad.CreateBuilding("Vasilis", "Mourtzis", new DateTime(1995, 3, 16), "0123456789", "1335", "Metro Aps", "32 Broadway Road");
            var bm = cad.FetchBuildingManager("1335");

            //Assert
            Assert.NotNull(building);
            Assert.Equal(building.BuildingName, "Metro Aps");
            Assert.Equal(building.Address, "32 Broadway Road");

            Assert.NotNull(bm);
            Assert.Equal(building.Id, bm.BuildingId);
            Assert.Equal(bm.FirstName, "Vasilis");
            Assert.Equal(bm.LastName, "Mourtzis");
            Assert.Equal(bm.DoB, new DateTime(1995, 3, 16));
            Assert.Equal(bm.Phone, "0123456789");

            //Erase Data
            cad.RemoveBuilding(building.Id);

        }
    }
}
