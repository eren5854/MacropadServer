using ED.GenericRepository;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;

namespace MacropadServer.Infrastructure.Services;
internal sealed class Generate(
    IMacropadInputRepository macropadInputRepository,
    IUnitOfWork unitOfWork) : IGenerate
{
    public void GenerateMacropadInput(MacropadDevice macropadDevice, MacropadModel macropadModel)
    {
        int buttonCount = macropadModel.ButtonCount;
        int modCount = macropadModel.ModCount;

        for (int i = 1; i <= modCount * buttonCount; i++)
        {
            MacropadInput macropadInput = new()
            {
                InputName = "Giriş " + i,
                InputIndex = i,
                ModIndex = ((i - 1) / buttonCount) + 1,
                InputBitMap = null,
                InputType = InputTypeEnum.Keyboard,
                Item1 = null,
                Item2 = null,
                Item3 = null,
                Item4 = null,
                MacropadDeviceId = macropadDevice.Id
            };
            macropadInputRepository.Add(macropadInput);
        }

        unitOfWork.SaveChanges();
    }

    public async Task<string> GenerateSecretToken()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+~";
        var random = new Random();
        int lenght = 64;
        string random2 = "";

        for (int i = 0; i < lenght; i++)
        {
            int a = random.Next(chars.Length);
            random2 = random2 + chars.ElementAt(a);
        }

        return random2;
    }

    public async Task<string> GenerateSerialNumber(MacropadModel macropadModel)
    {

        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var randomPart = new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        var date = DateOnly.FromDateTime(DateTime.Now).ToString("yyyyMMdd");
        var screenCode = macropadModel.IsScreenExist ? "DP" : "ND";
        string buttonCount = macropadModel.ButtonCount.ToString();
        if (macropadModel.ButtonCount < 10) buttonCount = "0" + macropadModel.ButtonCount;
        return $"MCP-{screenCode}-{buttonCount}-{date}-{randomPart}";

        //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //var random = new Random();
        //DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        //string formattedDate = date.ToString("yyyyMMdd");
        //int length = 6;
        //string random2 = "";
        //string serialNo = "MCP-";

        //for (int i = 0; i < length; i++)
        //{
        //    int a = random.Next(chars.Length);
        //    random2 = random2 + chars.ElementAt(a);
        //}

        //if (macropadModel.IsScreenExist)
        //{
        //    serialNo += "DP-";
        //}
        //if (!macropadModel.IsScreenExist)
        //{
        //    serialNo += "ND-";
        //}

        //return serialNo += macropadModel.ButtonCount + "-" + formattedDate + "-" + random2;

    }
}
