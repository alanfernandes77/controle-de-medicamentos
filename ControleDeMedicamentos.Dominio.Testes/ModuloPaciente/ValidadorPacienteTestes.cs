using ControleDeMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTestes
    {
        private Paciente? paciente;
        private readonly ValidadorPaciente validador;

        public ValidadorPacienteTestes()
        {
            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente = NovoPaciente();
            paciente.Nome = null;

            // action
            TestValidationResult<Paciente> resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            paciente = NovoPaciente();
            paciente.Nome = "Alan@-0g--gkdglsdgpo2";

            // action
            TestValidationResult<Paciente> resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.Nome);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente = NovoPaciente();
            paciente.CartaoSUS = null;

            // action
            TestValidationResult<Paciente> resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.CartaoSUS);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Valido()
        {
            // arrange
            paciente = NovoPaciente();
            paciente.CartaoSUS = "1674859GFGF687451450";

            // action
            TestValidationResult<Paciente> resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.CartaoSUS);
        }

        private Paciente NovoPaciente()
        {
            return new()
            {
                Nome = "Alan",
                CartaoSUS = "123456789123456"
            };
        }

    }
}