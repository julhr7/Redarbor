using System.Data;

namespace Redarbor.Application.Common.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}