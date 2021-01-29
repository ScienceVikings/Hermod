using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Hermod
{
    public class LambdaFunction
    {
        public string Handler(string input, ILambdaContext lambdaContext)
        {
            return input.ToUpper();
        }
    }
}