using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.VisualBasic.FileIO;
using Moq;
using NadinSoft.Application.Product.Commands.CreateProduct;
using NadinSoft.Application.Product.Commands.UpdateProduct;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Interfaces;
using System.Linq.Expressions;

namespace NadinSoft.Application.Test.Product
{
    public class CreateProductTest : IClassFixture<TestFixture>
    {
        [Theory]
        [InlineAutoMoqData(true)]
        [InlineAutoMoqData(false)]
        public void CommandSholdHaveUserIdToCreateSuccessful(
            bool commandHasUserId,
            CreateProductCommand command,
            [Frozen] Mock<IProductRepository> productRepository,
            CreateProductCommandHandler sut)
        {
            // Arrange
            productRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Domain.Entities.Product, bool>>>()))
                .ReturnsAsync(false);

            if (!commandHasUserId)
            {
                command = new CreateProductCommand(
                    null,
                    command.Name,
                    command.ProduceDate,
                    command.ManufacturePhone,
                    command.ManufactureEmail,
                    command.IsAvailable
                );
            }

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            if (commandHasUserId)
            {
                Assert.True(result.IsSuccess);
            }
            else
            {
                Assert.False(result.IsSuccess);
            }
        }

        [Theory]
        [InlineAutoMoqData]
        public void IfManufactureEmailIsDuplicatedShouldReturnFailure(
            CreateProductCommand command,
            [Frozen] Mock<IProductRepository> productRepository,
            CreateProductCommandHandler sut)
        {
            // Arrange
            productRepository.Setup(x => x.AnyAsync(It.Is<Expression<Func<Domain.Entities.Product, bool>>>(filter => filter.ToString().Replace(" ", "").Contains($".{nameof(Domain.Entities.Product.ManufactureEmail)}=="))))
                .ReturnsAsync(true);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineAutoMoqData]
        public void IfManufacturePhoneIsDuplicatedShouldReturnFailure(
            CreateProductCommand command,
            [Frozen] Mock<IProductRepository> productRepository,
            CreateProductCommandHandler sut)
        {
            // Arrange
            productRepository.Setup(x => x.AnyAsync(It.Is<Expression<Func<Domain.Entities.Product, bool>>>(filter => filter.ToString().Replace(" ", "").Contains($".{nameof(Domain.Entities.Product.ManufacturePhone)}=="))))
                .ReturnsAsync(true);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
        }
    }

    public class UpdateProductTest : IClassFixture<TestFixture>
    {

        [Theory]
        [InlineAutoMoqData(true)]
        [InlineAutoMoqData(false)]
        public void CommandSholdHaveUserIdToUpdateSuccessful(
                bool commandHasUserId,
                UpdateProductCommand command,
                [Frozen] Mock<IProductRepository> productRepository,
                UpdateProductCommandHandler sut)
        {
            // Arrange
            productRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Domain.Entities.Product, bool>>>()))
                .ReturnsAsync(false);

            if (!commandHasUserId)
            {
                command = new UpdateProductCommand(
                        command.Id,
                        null,
                        command.Name,
                        command.ProduceDate,
                        command.ManufacturePhone,
                        command.ManufactureEmail,
                        command.IsAvailable
                );
            }

            var product = new Domain.Entities.Product(
                command.UserId,
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable
            );

            productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(product);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            if (commandHasUserId)
            {
                Assert.True(result.IsSuccess);
            }
            else
            {
                Assert.False(result.IsSuccess);
            }
        }

        [Theory]
        [InlineAutoMoqData(true)]
        [InlineAutoMoqData(false)]
        public void CommandSenderUserIdShouldBeEqualWithProductUserIdToUpdateSuccessful(
                bool commandSenderUserIdIsEqualWithProductUserId,
                UpdateProductCommand command,
                [Frozen] Mock<IProductRepository> productRepository,
                UpdateProductCommandHandler sut)
        {
            // Arrange
            productRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Domain.Entities.Product, bool>>>()))
                .ReturnsAsync(false);

            var userId = command.UserId;

            if (!commandSenderUserIdIsEqualWithProductUserId)
            {
                userId = Guid.NewGuid().ToString();
            }

            var product = new Domain.Entities.Product(
                userId,
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable
            );

            productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(product);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            if (commandSenderUserIdIsEqualWithProductUserId)
            {
                Assert.True(result.IsSuccess);
            }
            else
            {
                Assert.False(result.IsSuccess);
            }

        }

        [Theory]
        [InlineAutoMoqData]
        public void IfManufactureEmailIsDuplicatedShouldReturnFailure(
            UpdateProductCommand command,
            [Frozen] Mock<IProductRepository> productRepository,
            UpdateProductCommandHandler sut)
        {
            // Arrange
            var product = new Domain.Entities.Product(
                command.UserId,
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable
            );

            productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(product);

            productRepository.Setup(x => x.AnyAsync(It.Is<Expression<Func<Domain.Entities.Product, bool>>>(filter => filter.ToString().Replace(" ", "").Contains($".{nameof(Domain.Entities.Product.ManufactureEmail)}=="))))
                .ReturnsAsync(true);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineAutoMoqData]
        public void IfManufacturePhoneIsDuplicatedShouldReturnFailure(
                UpdateProductCommand command,
                [Frozen] Mock<IProductRepository> productRepository,
                UpdateProductCommandHandler sut)
        {
            // Arrange
            var product = new Domain.Entities.Product(
                command.UserId,
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable
            );

            productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(product);

            productRepository.Setup(x => x.AnyAsync(It.Is<Expression<Func<Domain.Entities.Product, bool>>>(filter => filter.ToString().Replace(" ", "").Contains($".{nameof(Domain.Entities.Product.ManufacturePhone)}=="))))
                .ReturnsAsync(true);

            // Act
            var result = sut.Handle(command, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}