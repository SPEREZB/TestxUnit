using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestCodEjemplo;
using Xunit; 
using Moq; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
namespace TestxUnit
{
    public class UnitTest1
    {
        [Fact]
        public void GetOk()
        {
            // Arrange
            var dbContexts = new Dictionary<Type, DbContext>();
            var uow = new UnitOfWork(dbContexts);
              
            // Act
            // Ejecutar el código que estás probando en el método uow.GetOk()
            var resultadoObtenido = uow.GetOk();
             
            // Assert
            // Verificar que el resultado del código ejecutado cumple con las expectativas, usando las aserciones de xUnit
            Assert.IsType<OkObjectResult>(resultadoObtenido); 

        }


        [Fact]
        public void BeginTransaction_Should_Throw_Exception_If_Context_Not_Registered()
        {
            // Arrange
            var dbContexts = new Dictionary<Type, DbContext>();
            var uow = new UnitOfWork(dbContexts);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => uow.BeginTransaction<DbContextTransaction>());
        }
         
      
      

        [Fact]
        public void Rollback_Should_Rollback_Transactions()
        {
            // Arrange
            var dbContexts = new Dictionary<Type, DbContext>();
            var mockDbContext = new Mock<DbContext>();
            var mockTransaction = new Mock<DbContextTransaction>();
             

            var uow = new UnitOfWork(dbContexts);

            // Configurar comportamiento del mock
            mockTransaction.Setup(t => t.Dispose());


            // Act & Assert  
            Assert.Throws<InvalidOperationException>(() => uow.BeginTransaction<DbContextTransaction>()); 
        }

        [Fact]
        public void PruebaCommit()
        {
            // Arrange
            DbContextTransaction obj = new DbContextTransaction();
   
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => obj.Commit());
        }

        [Fact]
        public void PruebaRollback()
        {
            // Arrange
            DbContextTransaction obj = new DbContextTransaction();


            // Act & Assert
            Assert.Throws<NotImplementedException>(() => obj.Rollback());
        }

        [Fact]
        public void GetRepository_Should_Return_Repository_Instance()
        {
            // Arrange
            var dbContexts = new Dictionary<Type, DbContext>();
            var mockDbContext = new Mock<DbContext>();
            dbContexts.Add(typeof(DbContextTransaction), mockDbContext.Object);

            var uow = new UnitOfWork(dbContexts);

            // Act
            var repository = uow.GetRepository<object, DbContextTransaction>();

            // Assert
            Assert.NotNull(repository);
            Assert.IsType<Repository<object, DbContextTransaction>>(repository);
        }


    }
}