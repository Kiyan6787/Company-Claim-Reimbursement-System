using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Domain.Services;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using ClaimsReimbursement.Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace nUnit_Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private Mock<IReimbursementRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private AdminServices _service;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IReimbursementRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new AdminServices(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetClaims_ShouldReturnMappedReimbursementDTOs()
        {
            var claims = new List<Reimbursement> { new Reimbursement(), new Reimbursement() };
            var reimbursementDTOs = new List<ReimbursementDTO> { new ReimbursementDTO(), new ReimbursementDTO() };

            _repositoryMock.Setup(r => r.GetClaims()).ReturnsAsync(claims);
            _mapperMock.Setup(m => m.Map<List<ReimbursementDTO>>(claims)).Returns(reimbursementDTOs);

            var result = await _service.GetClaims();

            Assert.That(reimbursementDTOs, Is.EqualTo(result));
            _repositoryMock.Verify(r => r.GetClaims(), Times.Once);
            _mapperMock.Verify(m => m.Map<List<ReimbursementDTO>>(claims), Times.Once);
        }
    }
}
