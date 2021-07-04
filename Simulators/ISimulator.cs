using Aspentech.HYSYS;

namespace Simulators
{
    public interface ISimulator
    {
        bool Run();
        bool CreateSimulator();
        void CloseSimulator();
        bool OpenCase(CaseInfo caseInfo);
        void CloseCase();
        void SaveCase();
        int GetProcessId();
        dynamic GetCaseVariable(string moniker);
        _SimulationCase GetActiveSimulationCase();

    }
}