using ControleDeMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTestes
    {
        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = null,
                CartaoSUS = "165165464"
            };

            ValidadorPaciente validador = new();

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = "Alan@-0g--gkdglsdgpo2",
                CartaoSUS = "165165464"
            };

            ValidadorPaciente validador = new();

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Obrigatorio()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = null
            };

            ValidadorPaciente validador = new();

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Campo 'Cartão SUS' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Valido()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "1674859GFGF687451450"
            };

            ValidadorPaciente validador = new();

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Cartão SUS informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

    }
}