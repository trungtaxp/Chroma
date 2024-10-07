namespace Chroma
{
    public class RohdeSchwarzCommands : ICommands
    {
        public string MeasureVoltage() => "MEAS:VOLT?";
        public string MeasureCurrent() => "MEAS:CURR?";
        public string SetVoltageRange() => "SOUR:VOLT:RANG";
        public string SetCurrentRange() => "SOUR:CURR:RANG";
        public string SetOutputState() => "OUTP:STAT";
        public string Identify() => "*IDN?";
        public string Reset() => "*RST";
        public string ClearStatus() => "*CLS";
        public string ReadStatusByte() => "*STB?";
        public string SelfTest() => "*TST?";
        public string FetchData() => "FETCH:DATA?";
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
    }
}