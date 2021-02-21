using FiledPaymentProcessor.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiledPaymentProcessor.Core.Validators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public const int SECURITY_NUMBER_ALLOWED_LENGTH = 3;
        public PaymentRequestValidator()
        {
            RuleFor(pr => pr.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Amount must be positive");

            RuleFor(pr => pr.ExpirationDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Card has expired");

            RuleFor(pr => pr.CardHolder)
                .NotNull()
                .NotEmpty().WithMessage("Cardholder is required");

            RuleFor(pr => pr.SecurityCode).Must(
                sc =>
                {
                    if (!string.IsNullOrWhiteSpace(sc)) if (sc.Length == SECURITY_NUMBER_ALLOWED_LENGTH) { return true; } else { return false; }
                    return true;
                }
                ).WithMessage("Security Code is required");


            RuleFor(pr => pr.CreditCardNumber)
                .NotNull()
                .Must( ccn => ValidateCreditCardNumberWithLuhn(ccn)).WithMessage("Credit Card Number is Invalid");
        }

        public bool ValidateCreditCardNumberWithLuhn(string ccnString)
        {
            if (!Int64.TryParse(ccnString, out Int64 ccn)) return false;
            int counter = 0;
            int luhnSum = 0;
            foreach (var number in ccnString)
            {
                if (++counter % 2 == 0)
                {
                    var luhnEvenIndexValue = int.Parse(number.ToString()) * 2;
                    if (luhnEvenIndexValue > 9) luhnEvenIndexValue -= 9;
                    luhnSum += luhnEvenIndexValue;
                }
                else luhnSum += int.Parse(number.ToString());
            }
            return luhnSum % 10 == 0;
        }
    }
}