using System.ComponentModel.DataAnnotations;
using TCKimlikValidationAttribute.Attributes;

namespace TCKimlikValidationAttribute.Attributes.Models
{
    public class PersonModel
    {
        [TCKimlikValidation]
        public string TCKimlik { get; set; }
    }
}


/*
    Geçerli T.C. Kimlik Numarası: 10000000146
    Geçersiz T.C. Kimlik Numarası: 12345678910
*/ 