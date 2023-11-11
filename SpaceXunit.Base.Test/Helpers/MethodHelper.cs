namespace SpaceXunit.Base.Test.Helpers;

public static class MethodHelper
{
    public static (MethodInfo? Method, object[] Parameters) GetPrivateMethodInfo<T>(string nameOfMethod, params object[] parameters)
    {
        Type type = typeof(T);

        // Find all method using same name
        MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name == nameOfMethod)
            .ToArray();

        // Find a method that match the number of parameters and types of parameters
        MethodInfo? matchingMethod = methods.FirstOrDefault(method =>
        {
            var methodParams = method.GetParameters();

            // return if the parameters length is not matching
            if (methodParams.Length != parameters.Length)
                return false;

            for (int i = 0; i < methodParams.Length; i++)
            {
                Type paramType = parameters[i].GetType();
                Type methodParamType = methodParams[i].ParameterType;

                if (parameters[i] == null)
                {
                    if (!methodParamType.IsValueType || Nullable.GetUnderlyingType(methodParamType) != null)
                        continue;
                    else
                        return false;
                }

                return paramType == (Nullable.GetUnderlyingType(methodParamType) ?? methodParamType) ? true : false;
            }

            return true;
        });

        if (matchingMethod == null)
            return (null, parameters);

        ParameterInfo[] parameterInfo = matchingMethod.GetParameters();
        Type[] types = parameterInfo.Select(p => p.ParameterType).ToArray();

        return (typeof(T).GetMethod(nameOfMethod, (BindingFlags.NonPublic | BindingFlags.Instance), null, types, null), parameters);
    }
}
