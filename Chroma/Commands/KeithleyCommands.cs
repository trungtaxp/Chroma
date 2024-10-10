namespace Chroma.Commands
{
    public class KeithleyCommands : ICommands
    {
        public string MeasureVoltage() => ":MEAS:VOLT:DC?";
        public string MeasureVoltagAc() => ":MEAS:VOLT:AC?";
        public string MeasureCurrent() => ":MEAS:CURR?";
        public string SetVoltageRange() => ":SENS:VOLT:RANG";
        public string SetCurrentRange() => ":SENS:CURR:RANG";
        public string SetOutputState() => ":OUTP:STAT";
        public string Identify() => "*IDN?";
        public string Reset() => "*RST";
        public string ClearStatus() => "*CLS";
        public string ReadStatusByte() => "*STB?";
        public string SelfTest() => "*TST?";
        public string QuerySystemError() => "SYST:ERR?";
        public string QuerySystemVersion() => "SYST:VERS?";
        public string QuerySystemTime() => "SYST:TIME?";
        public string QuerySystemDate() => "SYST:DATE?";
        public string SetSystemLanguage() => "SYST:LANG";
        public string SetSystemLocal() => "SYST:LOC";
        public string SetSystemRemote() => "SYST:REM";
        public string GenerateSystemBeep() => "SYST:BEEP";
        public string LockFrontPanel() => "SYST:LOCK";
        public string UnlockFrontPanel() => "SYST:UNLOCK";
        public string DisplayText() => "DISP:TEXT";
        public string ClearDisplay() => "DISP:CLE";
        public string TriggerImmediate() => "TRIG:IMM";
        public string Initialize() => "INIT";
        public string Abort() => "ABOR";
        public string QueryOperationCondition() => "STAT:OPER:COND?";
        public string QueryOperationEvent() => "STAT:OPER:EVEN?";
        public string SetOperationEnable() => "STAT:OPER:ENAB";
        public string QueryQuestionableCondition() => "STAT:QUES:COND?";
        public string QueryQuestionableEvent() => "STAT:QUES:EVEN?";
        public string SetQuestionableEnable() => "STAT:QUES:ENAB";
        public string MeasureResistance() => ":MEAS:RES?";
        public string MeasureFrequency() => ":MEAS:FREQ?";
        public string MeasureTemperature() => ":MEAS:TEMP?";
        public string FetchReading() => ":FETCh?";
        public string RequestFreshReading() => ":SENS:DATA:FRES?";
        public string SetFunctionVoltageDc() => ":SENS:FUNC:VOLT:DC";
        public string SetFunctionVoltageAc() => ":SENS:FUNC:VOLT:AC";
        public string SetFunctionCurrentDc() => ":SENS:FUNC:CURR:DC";
        public string SetFunctionCurrentAc() => ":SENS:FUNC:CURR:AC";
        public string SetFunctionResistance() => ":SENS:FUNC:RES";
        public string SetFunctionFrequency() => ":SENS:FUNC:FREQ";
        public string SetFunctionTemperature() => ":SENS:FUNC:TEMP";
    }
}