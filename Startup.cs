using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace PlayMe
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var question = WebUtility.UrlDecode(context.Request.QueryString.ToString());
                var indexClean = question.IndexOf(": ") + 2;
                var questionClean = question.Substring(indexClean, question.Length - indexClean);
                
               Console.WriteLine(questionClean);

               if ( questionClean.Contains("largest"))
               {
                   var values = questionClean.Split(": ")[1].Split(", ");
                   var intValue = values.Select(x => int.Parse(x)).Max();
                    Console.WriteLine($"Response {intValue}");
                   await context.Response.WriteAsync(intValue.ToString());
               }else if (questionClean.Contains("plus"))
               {
                   var values = questionClean
                       .Replace("what is ", string.Empty)
                       .Replace(" plus", string.Empty)
                       .Split(" ");

                   var result =  values.Select(x => int.Parse(x)).Sum();
                   
                   Console.WriteLine($"Response {result}");
                   await context.Response.WriteAsync(result.ToString());
                   
               }
               else if(questionClean.Contains("multiplied by"))

               {
                   
                   var values = questionClean
                       .Replace("what is ", string.Empty)
                       .Replace(" multiplied by", string.Empty)
                       .Split(" ").Select(x=> int.Parse(x)).ToList();

                   var result = values[0] * values[1];
                   
                   Console.WriteLine($"Response {result}");
                   await context.Response.WriteAsync(result.ToString());
                   
               }else if (questionClean.Contains("both a square and a cube"))
               {

                   var nums = questionClean.Split(": ")[1].Split(", ").Select(int.Parse);
                   var result = string.Empty;
                   foreach (var num in nums)
                   {
                       
                       var isSquare = false;
                       var isCube = false;
                       for (var i = 0; i < num; i++)
                       {
                           if (i * i == num)
                           {
                               isSquare = true;
                           }

                           if (i * i * i == num)
                           {
                               isCube = true;
                           }
                       }

                       if (isSquare && isCube)
                       {
                           result = num.ToString();
                           break;
                       }
                   }

                   Console.WriteLine($"Response {result}");
                   await context.Response.WriteAsync(result.ToString());

               }

               else
               {
                   Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$LOOSING MONEY$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                   await context.Response.WriteAsync(string.Empty);
               }


            });
            
        }
    }
}