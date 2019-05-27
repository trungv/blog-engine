using Blog.Api.Configuration;
using Microsoft.Extensions.Options;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Blog.Api.Command
{
    public class Command<T> : ICommand<T> where T : class
    {
        private IDriver _driver;
        private readonly DbConfig _DbConfiguration;
        public Command(IOptions<DbConfig> myConfiguration)
        {
            _DbConfiguration = myConfiguration.Value;
            _driver = GraphDatabase.Driver(_DbConfiguration.Uri, AuthTokens.Basic(_DbConfiguration.Username, _DbConfiguration.Password));
        }

        public IEnumerable<T> WriteTransaction(string queryString, object param)
        {
            var returnList = new List<T>();
            try
            {
                using (var session = _driver.Session())
                {
                    session.WriteTransaction(tx =>
                    {
                        var result = tx.Run(queryString, param);
                        foreach (var record in result)
                        {
                            var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                            returnList.Add(JsonConvert.DeserializeObject<T>(nodeProps));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return returnList;
        }

        public bool ExcuteTransaction(string queryString, object param)
        {
            try
            {
                using (var session = _driver.Session())
                {
                    session.WriteTransaction(tx =>
                    {
                        var result = tx.Run(queryString, param);
                    });
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
