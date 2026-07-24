using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblTitleManagedStatistic
    {
        public string StatisticName { get; set;  }
        public XblTitleManagedStatType StatisticType { get; set; }
        public double NumberValue { get; set; }
        public string StringValue { get; set; }

        internal XblTitleManagedStatistic(Interop.XblTitleManagedStatistic interopStruct)
        {
            this.StatisticName = interopStruct.statisticName.GetString();
            this.StatisticType = interopStruct.statisticType;
            this.NumberValue = interopStruct.numberValue;
            this.StringValue = interopStruct.stringValue.GetString();
        }

        internal XblTitleManagedStatistic(string statisticName, XblTitleManagedStatType statType, string stringValue, double numberValue)
        {
            this.StatisticName = statisticName;
            this.StatisticType = statType;
            this.StringValue = stringValue;
            this.NumberValue = numberValue;
        }

        public XblTitleManagedStatistic()
        {
        }

        public XblTitleManagedStatistic(string statisticName, string statisticValue)
            : this(statisticName, XblTitleManagedStatType.String, statisticValue, default(double))
        {
        }

        public XblTitleManagedStatistic(string statisticName, double statisticValue)
            : this(statisticName, XblTitleManagedStatType.Number, default(string), statisticValue)
        {
        }

        public static Int32 Create(string statisticName, string statisticValue, out XblTitleManagedStatistic titleManagedStatistic)
        {
            titleManagedStatistic = new XblTitleManagedStatistic(statisticName, XblTitleManagedStatType.String, statisticValue, default(double));
            return HR.S_OK;
        }

        public static Int32 Create(string statisticName, double statisticValue, out XblTitleManagedStatistic titleManagedStatistic)
        {
            titleManagedStatistic = new XblTitleManagedStatistic(statisticName, XblTitleManagedStatType.Number, default(string), statisticValue);
            return HR.S_OK;
        }
    }
}
