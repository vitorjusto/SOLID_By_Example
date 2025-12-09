using SOLID_By_Example.S_SingleResponsability.After.Entities;
using SOLID_By_Example.S_SingleResponsability.After.Interfaces;
using SOLID_By_Example.S_SingleResponsability.After.UoW;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SOLID_By_Example.S_SingleResponsability.After.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly UnitOfWork _uow;

        public OrderRepository(UnitOfWork uow)
            => _uow = uow;

        public void Save(Order order)
        {
            using (var connection = new SqlConnection(_uow.Connection.ConnectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = _uow.Transaction)
                {
                    try
                    {
                        RegisterOrder(order, connection, transaction);
                        RegisterItem(order, connection, transaction);

                        transaction.Commit();
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Failed to register the order.{Environment.NewLine} Message: {ex.Message}");
                    }
                }
            }
        }

        private void RegisterOrder(Order order, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = $"INSERT INTO Orders (Id, Customer, Total) VALUES (@IdOrder, @Customer, @Total)";
                command.Parameters.Add("@IdOrder", SqlDbType.Int).Value = order.IdOrder;
                command.Parameters.Add("@Customer", SqlDbType.VarChar).Value = order.Customer;
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = order.Total;

                command.ExecuteNonQuery();
            }
        }

        private void RegisterItem(Order order, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var item in order.Items)
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = $"INSERT INTO PedidoItens (PedidoId, Quantidade, Preco) VALUES (@IdOrder, @Amount, @Price)";
                    command.Parameters.Add("@IdOrder", SqlDbType.Int).Value = order.IdOrder;
                    command.Parameters.Add("@IdOrder", SqlDbType.Int).Value = item.Amount;
                    command.Parameters.Add("@IdOrder", SqlDbType.Decimal).Value = item.Price;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}