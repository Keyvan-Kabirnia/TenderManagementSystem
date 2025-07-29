using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Configurations;
public class DbConfiguration
{
    public string Server { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string DbName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool TrustServerCertificate { get; set; }
    public bool MultipleActiveResultSets { get; set; }

    public string ConnectionString =>
        $"Server={Server},{Port};Database={DbName};User Id={UserId};Password={Password};TrustServerCertificate={TrustServerCertificate};MultipleActiveResultSets={MultipleActiveResultSets}";
}