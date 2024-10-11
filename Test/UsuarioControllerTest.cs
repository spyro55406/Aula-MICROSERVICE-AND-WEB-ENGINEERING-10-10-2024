using Moq;
using web_app_repository;
using web_app_performance.Controllers;
using wep_app_domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Test
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();  
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }

        [Fact]

        public async Task Get_UsuariosOk()
        {
            //Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Guilherme Miguel"
                },
            };

            _userRepositoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);

            //Act
            var result = await  _controller.GetUsuario();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]

        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarUsuarios())
                .ReturnsAsync((IEnumerable<Usuario>)null);

            var result = await _controller.GetUsuario();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarUsuario()
        {

            //Arrange
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "teste@fiap.com",
                Nome = "Gulherme Miguel"
            };

            _userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.Post(usuario);

            //Assert
            _userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);

        }
        
    }
}
