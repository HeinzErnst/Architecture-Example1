using System.Configuration;
using Domain.Products;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Persistance.Map;

namespace Persistance.Session
{
    internal class DbConfiguration
    {
        internal static FluentConfiguration Get()
        {
            return new DbConfiguration().GetConfiguration();
        }

        private DbConfiguration()
        {
        }

        private FluentConfiguration GetConfiguration()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            var msSqlConfiguration = MsSqlConfiguration.MsSql2012;
            msSqlConfiguration.ShowSql();

            msSqlConfiguration.ConnectionString(connectionString);

            var fluentConfiguration = Fluently.Configure().Database(msSqlConfiguration);

            fluentConfiguration.Mappings(m => m.FluentMappings.AddFromAssemblyOf<CategoryMap>());

            //var showSql = SettingsFromAppConfigFile();
            //fluentConfiguration.ExposeConfiguration(c => c.DataBaseIntegration(prop =>
            //{
            //    prop.BatchSize = 50;
            //    prop.LogSqlInConsole = showSql;
            //    prop.LogFormattedSql = showSql;
            //}));

            //CreateSchema(fluentConfiguration, true);

            return fluentConfiguration;
        }

        private static void CreateSchema(FluentConfiguration fluentConfiguration, bool update)
        {
            if (update)
            {
                fluentConfiguration.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, update));
            }
            else
            {
                var schema = new SchemaExport(fluentConfiguration.BuildConfiguration());
                //schema.Create(true, true);
            }
        }

        private bool SettingsFromAppConfigFile()
        {
            var configuration = new NHibernate.Cfg.Configuration();
            var showSqlstring = configuration.GetProperty("show_sql");
            var showSql = false;
            if (!string.IsNullOrEmpty(showSqlstring))
            {
                showSql = showSqlstring.ToUpper() == "TRUE";
            }

            return showSql;
        }
    }
}


