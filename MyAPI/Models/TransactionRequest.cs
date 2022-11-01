using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Models;

public class TransactionRequest
{
	public string Name { get; set; }
	public string CardNumber { get; set; }
	public string ExpirationDate { get; set; }
	public string SecurityCode { get; set; }
}
