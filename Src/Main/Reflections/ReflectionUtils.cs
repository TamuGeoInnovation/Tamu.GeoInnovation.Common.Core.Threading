using System;
using System.Reflection;
using System.Text;

namespace USC.GISResearchLab.Common.Utils.Reflections
{
    public class ReflectionUtils
    {
        public static string[][] GetObjectProperties(object instance)
        {
            string[][] ret = null;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (instance != null)
                {
                    Type type = instance.GetType();
                    PropertyInfo[] properyInfos = type.GetProperties();

                    if (properyInfos != null)
                    {
                        ret = new string[properyInfos.Length][];

                        for (int i = 0; i < properyInfos.Length; i++)
                        {
                            PropertyInfo properyInfo = properyInfos[i];

                            if (properyInfo != null)
                            {
                                string propertyName = properyInfo.Name;
                                Type propertyType = properyInfo.PropertyType;
                                object propertyValue = properyInfo.GetValue(instance, new Object[] { });

                                ret[i] = new string[2];
                                ret[i][0] = propertyName;
                                if (propertyValue != null)
                                {
                                    ret[i][1] = propertyValue.ToString();
                                }
                                else
                                {
                                    ret[i][1] = "null";
                                }
                            }
                            else
                            {
                                string here = "";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting object info", e);
            }
            return ret;
        }

        public static string GetObjectInfo(object instance)
        {
            string ret;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                Type type = instance.GetType();
                PropertyInfo[] properyInfos = type.GetProperties();

                if (properyInfos != null)
                {

                    stringBuilder.Append("Object Properties: ");
                    stringBuilder.Append(Environment.NewLine);
                    for (int i = 0; i < properyInfos.Length; i++)
                    {
                        PropertyInfo properyInfo = properyInfos[i];

                        string propertyName = properyInfo.Name;
                        Type propertyType = properyInfo.PropertyType;
                        object propertyValue = properyInfo.GetValue(instance, new Object[] { });

                        stringBuilder.Append("\t");
                        stringBuilder.AppendFormat("Propety Name: {0} Type = {1} Value = {2}",
                                propertyName,
                                propertyType,
                                propertyValue
                            );
                        stringBuilder.Append(Environment.NewLine);
                    }
                }
                ret = stringBuilder.ToString();
            }
            catch (Exception e)
            {
                throw new Exception("Error getting object info", e);
            }
            return ret;
        }


        public static string GetMethodInfo(MethodBase methodBase)
        {
            string ret = null;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(methodBase.Name);
                stringBuilder.Append("()");

                /*
                ParameterInfo[] parameters = methodBase.GetParameters();
                int parameterIndex = 0;
                while (parameterIndex < parameters.Length)
                {
                    stringBuilder.Append(parameters[parameterIndex].ParameterType.Name);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(parameters[parameterIndex].Name);
                    parameterIndex++;
                    if (parameterIndex != parameters.Length) stringBuilder.Append(", ");
                }
                */

                ret = stringBuilder.ToString();

            }
            catch (Exception e)
            {
                throw new Exception("Error getting method info", e);
            }
            return ret;
        }
    }
}
