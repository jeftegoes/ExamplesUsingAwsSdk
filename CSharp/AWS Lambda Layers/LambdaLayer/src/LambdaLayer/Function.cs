using Amazon.Lambda.Core;
using ExampleSimpleLib;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaLayer;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(Input input, ILambdaContext context)
    {
        var libraryUtils = new LibraryUtils();
        libraryUtils.PrintDateTime();
        return JsonConvert.SerializeObject(input);
    }
}
