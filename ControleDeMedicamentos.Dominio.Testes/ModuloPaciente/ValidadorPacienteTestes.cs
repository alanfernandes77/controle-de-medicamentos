using ControleDeMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTestes
    {
        private readonly Paciente paciente;
        private readonly ValidadorPaciente validador;

        public ValidadorPacienteTestes()
        {
            paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "123456789123456"
            };

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.Nome = null;

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            paciente.Nome = "Alan@-0g--gkdglsdgpo2";

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.CartaoSUS = null;

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Campo 'Cartão SUS' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Valido()
        {
            // arrange
            paciente.CartaoSUS = "1674859GFGF687451450";

            // action
            ValidationResult resultado = validador.Validate(paciente);

            // assert
            Assert.AreEqual("Cartão SUS informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

    }
}