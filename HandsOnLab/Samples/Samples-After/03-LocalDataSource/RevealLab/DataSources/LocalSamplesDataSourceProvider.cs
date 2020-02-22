using Infragistics.Sdk;
using System.Threading.Tasks;

namespace RevealLab.DataSources
{
    class LocalSamplesDataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDashboardFilterDataSourceItemAsync(RVDashboardFilter filter, RVDataSourceItem dataSourceItem)
        {
            if (IsSamplesWebResource(dataSourceItem))
            {
                return Task.FromResult(CreateLocalSamplesDataSourceItem(dataSourceItem));
            }
            else
            {
                return Task.FromResult<RVDataSourceItem>(null);
            }
        }

        public Task<RVDataSourceItem> ChangeVisualizationDataSourceItemAsync(RVVisualization visualization, RVDataSourceItem dataSourceItem)
        {
            if (IsSamplesWebResource(dataSourceItem))
            {
                return Task.FromResult(CreateLocalSamplesDataSourceItem(dataSourceItem));
            }
            else
            {
                return Task.FromResult<RVDataSourceItem>(null);
            }
        }

        private bool IsSamplesWebResource(RVDataSourceItem dataSourceItem)
        {
            if (dataSourceItem is RVExcelDataSourceItem excelItem)
            {
                var wrItem = excelItem.ResourceItem as RVWebResourceDataSourceItem;
                return wrItem != null && wrItem.Url.EndsWith("Samples.xlsx");
            }

            return false;            
        }

        private RVDataSourceItem CreateLocalSamplesDataSourceItem(RVDataSourceItem dataSourceItem)
        {
            if (dataSourceItem is RVExcelDataSourceItem excelItem)
            {
                var localItem = new RVLocalFileDataSourceItem();
                localItem.Uri = "local:/Samples.xlsx";
                excelItem.ResourceItem = localItem;
                return excelItem;
            }

            return dataSourceItem;
        }
    }
}
