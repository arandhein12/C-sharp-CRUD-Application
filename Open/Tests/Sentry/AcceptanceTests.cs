using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Open.Aids;
using Open.Domain.Country;
using Open.Infra.Country;
using Open.Sentry;

namespace Open.Tests.Sentry{
 public   abstract class AcceptanceTests {
     protected readonly Assembly Assembly;
     protected readonly HttpClient Client;
     protected ICountryObjectsRepository repository;
     protected string path;

     protected AcceptanceTests() {
         Assembly = typeof(Startup).GetTypeInfo().Assembly;
         path = getPath();
         var server = new TestServer(new WebHostBuilder()
             .UseContentRoot(path)
             .UseStartup<TestStartup>()
             .ConfigureServices(services => configure(services, Assembly))
         );
         initTestDatabase(server);
         Client = server.CreateClient();
     }
     private void initTestDatabase(TestServer server) {
         using (var scope = server.Host.Services.CreateScope()) {
             var services = scope.ServiceProvider;
             repository = services.GetRequiredService<ICountryObjectsRepository>();
             CountryDbTableInitializer.Initialize(repository);
         }
     }
     private static void configure(IServiceCollection services, Assembly a) {
         services.Configure((RazorViewEngineOptions options) => {
             var previous = options.CompilationCallback;
             options.CompilationCallback = (context) => {
                 previous?.Invoke(context);
                 var assemblies = a.GetReferencedAssemblies().Select(x =>
                         MetadataReference.CreateFromFile(Assembly.Load(x).Location))
                     .ToList();
                 addReference(assemblies, "netstandard");
                 addReference(assemblies, "System.Private.Corelib");
                 addReference(assemblies, "Microsoft.AspNetCore.Html.Abstractions");
                 addReference(assemblies, "System.Linq.Expressions");
                 context.Compilation = context.Compilation.AddReferences(assemblies);

             };

             });
     }
     private static void addReference(List<PortableExecutableReference> list,
         string assemblyName) {
         list.Add(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName(assemblyName)) .Location));
     }
     private string getPath() {
         var n = GetString.Tail(Assembly.GetName().Name);
         var p = PlatformServices.Default.Application.ApplicationBasePath;
         var d = new DirectoryInfo(p);
         while (d != null) {
             var f = new FileInfo(Path.Combine(d.FullName, "Open.sln"));
             if (f.Exists) return Path.GetFullPath(Path.Combine(d.FullName, n));
             d = d.Parent;

         }
         throw new Exception($"No solution file in path <{p}>.");
     }
 }
}
