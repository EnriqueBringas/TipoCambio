using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TipoCambio.Api.Backend.Data.Code.Helpers
{
    public static class SqlExtensions
    {
        public static IEnumerable<T> HydrateRows<T>(this IDataReader reader) where T : new()
        {
            var hashtable = GetProperties<T>();
            var entities = new List<T>();

            while (reader.Read())
            {
                T newObject = reader.GetFields<T>(hashtable);

                entities.Add(newObject);
            }

            return entities;
        }

        public static T HydrateFields<T>(this IDataReader reader) where T : new()
        {
            var hashtable = GetProperties<T>();
            var entities = new List<T>();

            while (reader.Read())
            {
                T newObject = reader.GetFields<T>(hashtable);

                entities.Add(newObject);
            }

            return entities.Any() ? entities.FirstOrDefault() : new();
        }

        private static T GetFields<T>(this IDataReader reader, Hashtable properties = null) where T : new()
        {
            PropertyInfo info;
            T entity = new();

            if (properties == null)
                properties = GetProperties<T>();

            for (int index = 0; index < reader.FieldCount; index++)
            {
                info = (PropertyInfo)properties[reader.GetName(index)];

                if ((info != null) && info.CanWrite)
                {
                    try
                    {
                        info.SetValue(entity, reader.GetValue(index) == DBNull.Value ? default(T) : reader.GetValue(index), null);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(String.Format("Error de comptabilidad en la asignación del campo [Origen (Tipo) -> Destino (Tipo)] : {0}({1}) -> {2}({3})",
                                            reader.GetName(index).ToString(),
                                            reader.GetValue(index).GetType().Name,
                                            info.Name,
                                            info.PropertyType.Name
                                            ),
                                            e);
                    }
                }
            }

            return entity;
        }

        private static Hashtable GetProperties<T>() where T : new()
        {
            var properties = new Hashtable();
            var businessEntityType = typeof(T);
            var propertiesInfo = businessEntityType.GetProperties();

            foreach (var property in propertiesInfo)
            {
                properties[property.Name] = property;
            }

            return properties;
        }
    }
}