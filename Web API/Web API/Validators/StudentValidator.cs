using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Web_API.Models;

namespace Web_API.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            //RuleFor(x => x.Id).NotEmpty().WithMessage("Id is word is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date Of Birth is required.");
            //RuleFor(x => x.Sex).NotEmpty().WithMessage("Sex is required.");
            RuleFor(x => x.NativeVillage).NotEmpty().WithMessage("Native Village is required.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail is required.");
        }

    }
}
