using System.Reflection;
using System.Reflection.Emit;
using TypeBuilder = System.Reflection.Emit.TypeBuilder;

namespace BlackDigital.Rest
{
    public class RestServiceBuilder
    {
        #region "Constructors"

        public RestServiceBuilder() { }

        #endregion "Constructors"

        #region "Properties"

        private List<Type> Services = new();
        private const string ASSEMBLYNAME = "BlackDigital.Rest.Services";

        #endregion "Properties"

        #region "Public Methods"

        public RestServiceBuilder AddService<T>() => AddService(typeof(T));

        public RestServiceBuilder AddService(Type service)
        {
            Services.Add(service);
            return this;
        }

        public Dictionary<Type, Type> Build()
        {
            Dictionary<Type, Type> servicesBuilded = new();
            AssemblyName assemblyName = new(ASSEMBLYNAME);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            foreach (var interfaceType in Services)
            {
                servicesBuilded.Add(interfaceType, BuildType(moduleBuilder, interfaceType));
            }

            return servicesBuilded;
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Type BuildType(ModuleBuilder moduleBuilder, Type interfaceType)
        {
            var baseServiceType = typeof(BaseService<>);
            var baseType = baseServiceType.MakeGenericType(new Type[] { interfaceType });

            TypeBuilder typeBuilder = moduleBuilder.DefineType($"{ASSEMBLYNAME}.{interfaceType.Name}RestService", TypeAttributes.Public, baseType);
            typeBuilder.AddInterfaceImplementation(interfaceType);
            CreateConstructor(typeBuilder, baseType);

            foreach (var method in interfaceType.GetMethods())
            {
                CreateInterfaceMethod(typeBuilder, method, baseType);
            }

            return typeBuilder.CreateType();
        }

        private static void CreateConstructor(TypeBuilder typeBuilder, Type baseType)
        {
            ConstructorBuilder constructor = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                           CallingConventions.Standard,
                                                                           new Type[] { typeof(RestClient) });

            ILGenerator ilConstructor = constructor.GetILGenerator();

            ilConstructor.Emit(OpCodes.Ldarg_0);
            ilConstructor.Emit(OpCodes.Ldarg_1);
            ilConstructor.Emit(OpCodes.Call, baseType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(RestClient) }, null));
            ilConstructor.Emit(OpCodes.Nop); //TODO: remove??
            ilConstructor.Emit(OpCodes.Nop); //TODO: remove??
            ilConstructor.Emit(OpCodes.Ret);
        }

        private static void CreateInterfaceMethod(TypeBuilder typeBuilder, MethodInfo method, Type baseType)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name,
                                                                       MethodAttributes.Public | MethodAttributes.Virtual,
                                                                       method.ReturnType,
                                                                       method.GetParameters().Select(x => x.ParameterType)
                                                                       .ToArray());

            ILGenerator il = methodBuilder.GetILGenerator();
            //var listParamatersType = typeof(MethodParameters);
            var listParamatersType = typeof(Dictionary<string, object>);

            il.Emit(OpCodes.Nop);
            var dictParameters = il.DeclareLocal(listParamatersType);
            il.Emit(OpCodes.Newobj, listParamatersType.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, dictParameters);

            foreach (var parameter in method.GetParameters())
            {
                CreateMethodParametersAdd(parameter, il, dictParameters, listParamatersType);
            }

            if (method.ReturnType != typeof(void))
                CreateMethodReturnWithValue(method, il, baseType, dictParameters);
            else
                CreateMethodReturnWithoutValue(method, il, baseType, dictParameters);

            il.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodBuilder, method);
        }

        private static void CreateMethodParametersAdd(ParameterInfo parameter, 
                                                      ILGenerator il, 
                                                      LocalBuilder dictParameters,
                                                      Type dictParametersType)
        {
            if (parameter.Position > 0)
                il.Emit(OpCodes.Nop); //TODO: remove??


            il.Emit(OpCodes.Ldloc, dictParameters);
            il.Emit(OpCodes.Ldstr, parameter.Name);
            il.Emit(OpCodes.Ldarg, parameter.Position + 1);

            if (parameter.ParameterType.IsValueType)
                il.Emit(OpCodes.Box, parameter.ParameterType);

            il.Emit(OpCodes.Callvirt, dictParametersType.GetMethod("Add"));
        }

        private static void CreateMethodReturnWithValue(MethodInfo method, 
                                                        ILGenerator il, 
                                                        Type baseType,
                                                        LocalBuilder dictParameters)
        {
            var executeRequestMethod = baseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                               .Single(x => x.Name == "ExecuteRequest"
                                                        && x.GetGenericArguments().Length == 1
                                                        && x.GetParameters().Length == 2);

            il.Emit(OpCodes.Nop); //TODO: remove??

            var returnType = method.ReturnType;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                returnType = returnType.GetGenericArguments()[0];

            var returnVar = il.DeclareLocal(returnType);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, method.Name);
            il.Emit(OpCodes.Ldloc, dictParameters);
            il.Emit(OpCodes.Call, executeRequestMethod.MakeGenericMethod(returnType));
            il.Emit(OpCodes.Stloc, returnVar);

            //il.Emit(OpCodes.Br_S);
            il.Emit(OpCodes.Ldloc, returnVar);
        }

        private static void CreateMethodReturnWithoutValue(MethodInfo method,
                                                        ILGenerator il,
                                                        Type baseType,
                                                        LocalBuilder dictParameters)
        {
            throw new NotImplementedException();
        }

        #endregion "Private Methods"
    }
}
