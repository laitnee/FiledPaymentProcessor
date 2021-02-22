using AutoMapper;
using FiledPaymentProcessor.Core.DTOs;
using FiledPaymentProcessor.Core.Entities;
using FiledPaymentProcessor.Core.Repositories;
using FiledPaymentProcessor.Core.Services;
using FiledPaymentProcessor.Core.Services.PaymentGateway;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Filed.PaymentProcessor.UnitTest
{
    public class PaymentServiceTest 
    {

        public IPaymentService _paymentService;
        public Mock<IUnitOfWork> mockUnitOfWork;
        public Mock<IPaymentGatewayFactory> mockPaymentGatewwayFactory;
        public PaymentServiceTest()
        {
            TestSetup();
        }

        [Fact]
        public void ShouldAddAndUpdatePaymentDetailsAndState()
        {
            PaymentRequest req = new PaymentRequest
            {
                CreditCardNumber = "4556031415183354",
                CardHolder = "Jane Doe",
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                SecurityCode = "342",
                Amount = 23.44M
            };
            _paymentService.ProcessPaymentRequest(req);
            mockUnitOfWork.Verify(x => x.Payments.Add(It.IsAny<PaymentDetail>()), Times.Once);
            mockUnitOfWork.Verify(x => x.PaymentState.Add(It.IsAny<PaymentStatus>()), Times.Once);
            mockUnitOfWork.Verify(x => x.PaymentState.Update(It.IsAny<PaymentStatus>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Complete(), Times.Exactly(2));
        }

        [Fact]
        public void ShouldUseCheapGatewayForAmountLesserOrEqualToTwenty()
        {

            PaymentRequest req = new PaymentRequest
            {
                CreditCardNumber = "4556031415183354",
                CardHolder = "Jane Doe",
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                SecurityCode = "342",
                Amount = 18.44M
            };
            _paymentService.ProcessPaymentRequest(req);
            
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateCheapGatewayCheapService(), Times.Once);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayExpensiveService(), Times.Never);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayPremiumService(), Times.Never);
            
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateCheapGatewayCheapService().PaymentProcessor(It.IsAny<PaymentRequest>()), Times.Once);

        }

        [Fact]
        public void ShouldUseExpensiveGatewayForAmountGreaterThanTwenty()
        {

            PaymentRequest req = new PaymentRequest
            {
                CreditCardNumber = "4556031415183354",
                CardHolder = "Jane Doe",
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                SecurityCode = "342",
                Amount = 26.44M
            };
            _paymentService.ProcessPaymentRequest(req);
            
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateCheapGatewayCheapService(), Times.Never);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayExpensiveService(), Times.Once);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayPremiumService(), Times.Never);

            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayExpensiveService().PaymentProcessor(It.IsAny<PaymentRequest>()), Times.Once);

        }

        [Fact]
        public void ShouldUseExpensiveGatewayForAmountGreaterThanFiveHundred()
        {

            PaymentRequest req = new PaymentRequest
            {
                CreditCardNumber = "4556031415183354",
                CardHolder = "Jane Doe",
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                SecurityCode = "342",
                Amount = 600.44M
            };
            _paymentService.ProcessPaymentRequest(req);

            mockPaymentGatewwayFactory.Verify(fac => fac.CreateCheapGatewayCheapService(), Times.Never);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayExpensiveService(), Times.Never);
            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayPremiumService(), Times.Once);

            mockPaymentGatewwayFactory.Verify(fac => fac.CreateExpensiveGatewayPremiumService().PaymentProcessor(It.IsAny<PaymentRequest>()), Times.Once);

        }

        private void TestSetup()
        {
            Mock<IPaymentDetailRepository> paymentDetailMockRepo = new Mock<IPaymentDetailRepository>();
            Mock<IPaymentStatusRepository> paymentStatusMockRepo = new Mock<IPaymentStatusRepository>();

            paymentDetailMockRepo.Setup(pd => pd.Add(It.IsAny<PaymentDetail>()));
            paymentStatusMockRepo.Setup(ps => ps.Add(It.IsAny<PaymentStatus>()));
            paymentStatusMockRepo.Setup(ps => ps.Update(It.IsAny<PaymentStatus>()));

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Payments).Returns(paymentDetailMockRepo.Object);
            mockUnitOfWork.Setup(uow => uow.PaymentState).Returns(paymentStatusMockRepo.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mp => mp.Map<PaymentDetail>(It.IsAny<PaymentRequest>())).Returns(
                (PaymentRequest req) =>
                {
                    var paymentDetail = new PaymentDetail
                    {
                        Amount = req.Amount,
                        CreditCardNumber = req.CreditCardNumber,
                        CardHolder = req.CardHolder,
                        ExpirationDate = req.ExpirationDate,
                        SecurityCode = req.SecurityCode
                    };
                    return paymentDetail;
                }
            );
            mockMapper.Setup(mp => mp.Map<PaymentResponseDTO>(It.IsAny<PaymentDetail>())).Returns(
                (PaymentDetail det) =>
                {
                    var paymentResponseDTO = new PaymentResponseDTO
                    {
                        Amount = det.Amount,
                        Id = det.Id,
                        PaymentState = det.Status.State.ToString()
                    };
                    return paymentResponseDTO;
                }
            );

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<PaymentService>();

            mockPaymentGatewwayFactory = new Mock<IPaymentGatewayFactory>();

            Mock<ICheapPaymentGateway> mockCheapPayment = new Mock<ICheapPaymentGateway>();
            mockCheapPayment.Setup(cp => cp.PaymentProcessor(It.IsAny<PaymentRequest>()));
            mockPaymentGatewwayFactory.Setup(fac => fac.CreateCheapGatewayCheapService()).Returns(mockCheapPayment.Object);

            Mock<IExpensivePaymentGateway> mockExpensivePayment = new Mock<IExpensivePaymentGateway>();
            mockCheapPayment.Setup(cp => cp.PaymentProcessor(It.IsAny<PaymentRequest>()));
            mockPaymentGatewwayFactory.Setup(fac => fac.CreateExpensiveGatewayExpensiveService()).Returns(mockCheapPayment.Object);
            mockPaymentGatewwayFactory.Setup(fac => fac.CreateExpensiveGatewayPremiumService()).Returns(mockCheapPayment.Object);

            _paymentService = new PaymentService(mockUnitOfWork.Object, mockMapper.Object, logger, mockPaymentGatewwayFactory.Object);
        }

    }
}
