using Inventory.Models;
using Inventory.Models.Enums;
using Inventory.Repositories.Interfaces;
using Inventory.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Inventory.Tests.Services
{
    public class AccessServiceTest
    {
        private Mock<ILogger<AccessService>> _mockLogger = new Mock<ILogger<AccessService>>();
        private Mock<IGenericRepository<AccessDTO>> _mockAccessRepo = new Mock<IGenericRepository<AccessDTO>>();

        [Fact]
        public void User_is_admin()
        {
            AccessService service = new AccessService(_mockLogger.Object, _mockAccessRepo.Object);
            _mockAccessRepo.Setup(e => e.Find(It.IsAny<Func<AccessDTO, bool>>())).Returns(new List<AccessDTO> { new AccessDTO() {
            accessId="admin",
            secretKey="secret123",
            role=Roles.Admin
            } });
            bool res = service.IsAdmin("admin", "secret123");
            Assert.True(res);
        }

        [Fact]
        public void Not_admin_user()
        {
            AccessService service = new AccessService(_mockLogger.Object, _mockAccessRepo.Object);
            _mockAccessRepo.Setup(e => e.Find(It.IsAny<Func<AccessDTO, bool>>())).Returns(new List<AccessDTO> { new AccessDTO() {
            accessId="admin",
            secretKey="secret789",
            role=Roles.User
            } });
            bool res = service.IsAdmin("admin", "secret123");
            Assert.False(res);
        }
    }
}
