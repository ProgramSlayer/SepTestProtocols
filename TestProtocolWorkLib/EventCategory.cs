using System;
using System.Collections.Generic;

namespace TestProtocolWorkLib
{
    public static class EventCategory
    {
        /// <summary>
        /// Перечисление с категориями событий.
        /// </summary>
        [Flags]
        public enum EventCategories
        {
            /// <summary>
            /// Событие не относится ни к какой категории.
            /// </summary>
            None = 0,

            /// <summary>
            /// Категория "Нарушитель".
            /// </summary>
            Intruder = 1,

            /// <summary>
            /// Категория "Инженерно-технические средства охраны (ИТСО)".
            /// </summary>
            SecurityDevices = 2,

            /// <summary>
            /// Категория "Оператор СБ".
            /// </summary>
            Operator = 4,

            /// <summary>
            /// Категория "Оператор СВН".
            /// </summary>
            OperatorCctv = 8,

            /// <summary>
            /// Категория "Группа реагирования".
            /// </summary>
            Guards = 16,

            /// <summary>
            /// Событие относится к категориям <see cref="EventCategories.Intruder"/> / <see cref="EventCategories.Guards"/>.
            /// </summary>
            IntruderGuards = Intruder | Guards,

            /// <summary>
            /// Событие относится к категориям <see cref="SecurityDevices"/> / <see cref="Operator"/>.
            /// </summary>
            SecurityDevicesOperator = SecurityDevices | Operator,

            /// <summary>
            /// Событие относится к категориям <see cref="Operator"/> / <see cref="Guards"/>.
            /// </summary>
            OperatorGuards = Operator | Guards,

            /// <summary>
            /// Событие относится к категориям <see cref="Intruder"/> / <see cref="SecurityDevices"/>.
            /// </summary>
            SecurityDevicesIntruder = Intruder | SecurityDevices,
        }

        public static readonly Dictionary<string, EventCategories> EventsCategoriesDictionary = new()
        {
            { "VisionRangeEvent", EventCategories.None },
            { "CaptureRadiusEvent", EventCategories.None },
            { "EntranceToZoneEvent", EventCategories.Intruder },
            { "ExitFromZoneEvent", EventCategories.Intruder },
            { "ReduceDetSensWeatherEvent", EventCategories.None },
            { "ReduceDetSensPartnerEvent", EventCategories.None },
            { "SignResetEvent", EventCategories.SecurityDevicesOperator },
            { "MovToPointEvent", EventCategories.Guards },
            { "SpecZoneNotAffVehEvent", EventCategories.None },
            { "SpecZoneAffVehEvent", EventCategories.None },
            { "SpeedLimitToEvent", EventCategories.IntruderGuards },
            { "SpeedMaxEvent", EventCategories.Guards }, // ?
            { "SpeedValueEvent", EventCategories.IntruderGuards },
            { "GetMarkingEvent", EventCategories.Guards },
            { "AssSitEvent", EventCategories.Guards },
            { "MoveToLastPointEvent", EventCategories.Guards },
            { "IntruderVisEvent", EventCategories.Intruder },
            { "IntruderVisThroughEvent", EventCategories.Intruder },
            { "InterceptStartEvent", EventCategories.Guards }, // ?
            { "SwitchingTo", EventCategories.Guards }, // ?
            { "VisContRestEvent", EventCategories.Guards },
            { "TimeGatheringEvent", EventCategories.Guards },
            { "MovToTargetEvent", EventCategories.Guards },
            { "RuMovStartInterceptEvent", EventCategories.Guards },
            { "RuMovInterceptEvent", EventCategories.Guards }, // ?
            { "LeadAngleChangedEvent", EventCategories.Guards },
            { "MarkingIgnEvent", EventCategories.Guards },
            { "CoordMeasurEvent", EventCategories.SecurityDevices }, // ?
            { "SignIssuedEvent", EventCategories.SecurityDevices },
            { "DetNotWorkEvent", EventCategories.SecurityDevices },
            { "FalseAlarmBackgrEvent", EventCategories.SecurityDevices },
            { "FalseAlarmWeatherEvent", EventCategories.SecurityDevices }, // ?
            { "RecogProbChangedEvent", EventCategories.SecurityDevicesOperator }, // ?
            { "MarkingForRuEvent", EventCategories.Operator }, // ?
            { "InspBeginEvent", EventCategories.Operator },
            { "InspEndEvent", EventCategories.Operator },
            { "ImgEvalStartEvent", EventCategories.Operator },
            { "IntruderFoundInCamZoneEvent", EventCategories.Operator }, // ?
            { "CamManualEvent", EventCategories.Operator }, // ?
            { "SwToCamSetEvent", EventCategories.Operator }, // ?
            { "ImgEvalEndEvent", EventCategories.Operator },
            { "CalcPropDetectEvent", EventCategories.Operator },
            { "NoDetectEvent", EventCategories.Operator },
            { "CamAutoEvent", EventCategories.Operator }, // ?
            { "MoveEvent", EventCategories.Operator },
            { "MoveToIntEvent", EventCategories.Operator }, // ?
            { "MoveToTargEvent", EventCategories.Operator },
            { "TargSuppEvent", EventCategories.Operator },
            { "IntruderDetectedEvent", EventCategories.Operator },
            { "MoveRuEvent", EventCategories.Operator }, // ?
            { "IntruderDetRuEvent", EventCategories.OperatorGuards }, // ?
            { "FalseDetectionEvent", EventCategories.Operator },
            { "AlarmEvent", EventCategories.Operator },
            { "TimeDetermAreaEvent", EventCategories.Operator },
            { "AreaDetermEvent", EventCategories.Operator },
            { "TimeAreaInspectEvent", EventCategories.Operator },
            { "AreaInspectEvent", EventCategories.Operator },
            { "TimeDecisionEvent", EventCategories.Operator },
            { "SignAcceptEvent", EventCategories.SecurityDevicesOperator }, // ?
            { "FalseAlarmEvent", EventCategories.Operator },
            { "AreaOutOfCamEvent", EventCategories.Operator },
            { "SpeedIncEvent", EventCategories.Intruder }, // ?
            { "SpeedDecEvent", EventCategories.Intruder }, // ?
            { "SpeedIncVehEvent", EventCategories.Intruder }, // ?
            { "SpeedDecEquipEvent", EventCategories.Intruder }, // ?
            { "RamOvercomeEvent", EventCategories.Intruder }, // ?
            { "ImpossOvercomeEvent", EventCategories.Intruder }, // ?
            { "ReducDelayEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "BarrierOverBeginEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "BarrierOverEndEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "BarrierOverMiddleEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "HurdleEntryBeginEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "HurdleEntryEndEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "HurdleExitBeginEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "HurdleExitEndEvent", EventCategories.SecurityDevicesIntruder }, // ?
            { "RuStartEvent", EventCategories.Guards },
            { "EndPointPatrolEvent", EventCategories.Guards }, // ?
            { "PostDelayEvent", EventCategories.Guards }, // ?
            { "ContinuePatrolEvent", EventCategories.Guards }, // ?
            { "BeginPatrolEvent", EventCategories.Guards }, // ?
            { "VehAbandEvent", EventCategories.Intruder },
            { "HeavyEquipAbandEvent", EventCategories.Intruder },
            { "MediumEquipAband", EventCategories.Intruder },
            { "EasyEquipAbandEvent", EventCategories.Intruder },
            { "VisContactEvent", EventCategories.Intruder },
            { "NonPenetEvent", EventCategories.IntruderGuards }, // ?
            { "ReducedCautionEvent", EventCategories.Intruder },
            { "DirectionChangedEvent", EventCategories.Intruder },
            { "DevEndEvent", EventCategories.Intruder },
            { "ContinMoveEvent", EventCategories.Intruder },
            { "PenetrationEvent", EventCategories.IntruderGuards }, // ?
            { "IntruderNeutralizedEvent", EventCategories.IntruderGuards }, // ?
            { "ExtSecurityCall", EventCategories.Operator },
            { "EstimatedArrivalTime", EventCategories.Guards },
            { "ExtSecurityArrival", EventCategories.Guards }, // ?
            { "SpecZoneEntryBeginEvent", EventCategories.None }, // ?
            { "SpecZoneEntryEndEvent", EventCategories.None }, // ?
            { "SpecZoneExitBeginEvent", EventCategories.None }, // ?
            { "SpecZoneExitEndEvent", EventCategories.None }, // ?
        };
    }
}
