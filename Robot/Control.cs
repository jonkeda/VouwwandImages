using System;
using System.Threading;
using System.Threading.Tasks;
using CefSharp;
using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot;

public class Control
{
    protected By By { get; }
    protected Context Context { get; }
    protected bool CheckExistence { get; } = true;

    public Control(Context context, By by)
    {
        Context = context;
        By = by;
    }

    public Control(Context context, By by, bool checkExistence)
    {
        Context = context;
        By = by;
        CheckExistence = checkExistence;
    }

    private void WaitDocumentReady()
    {
        int count = 0;
        while (Context.Browser.IsLoading && count < Context.WaitCount)
        {
            count++;
            Thread.Sleep(Context.WaitTime);
        }
    }

    private async Task WaitForElements(string selector)
    {
        if (!CheckExistence)
        {
            return;
        }

        WaitDocumentReady();
        bool notDone = true;
        int count = 0;
        while (notDone && count < 10)
        {
            var result =
                await Context.Browser.EvaluateScriptAsync($"{selector}");
            if (result.Success
                && (bool)result.Result)
            {
                return;
            }

            count++;
            Thread.Sleep(Context.WaitTime);
        }

        throw new Exception("Element not found: " + selector);
    }

    public async Task Click()
    {
        await WaitForElements(By.ExistsSelector());

        Context.Browser.ExecuteScriptAsync($"{By.GetSelector()}.click()");
    }

    public async Task Set(string text)
    {
        await WaitForElements(By.ExistsSelector());

        Context.Browser.ExecuteScriptAsync($"{By.GetSelector()}.value='{text}'");
    }

    public async Task<string> GetAttribute(string attribute, string value)
    {
        await WaitForElements(By.ExistsSelector());

        var response = await Context.Browser.EvaluateScriptAsync($"{By.GetSelector()}.getAttribute('{attribute}')");

        return (string)response.Result;
    }

    public async Task WaitForAttribute(string attribute, string value)
    {
        DateTime time = DateTime.Now;
        time = time.AddMinutes(1);
        while (time > DateTime.Now)
        {
            string foundValue = await GetAttribute(attribute, value);
            if (foundValue == value)
            {
                return;
            }

            Thread.Sleep(100);
        }
    }

    public async Task SetDisable(bool disabled)
    {
        await WaitForElements(By.ExistsSelector());

        Context.Browser.ExecuteScriptAsync($"{By.GetSelector()}.disabled={disabled.ToString().ToLower()}");
    }

    public async Task<string?> GetValue()
    {
        await WaitForElements(By.ExistsSelector());

        var response = await Context.Browser.EvaluateScriptAsync($"{By.GetSelector()}.innerText");
        return (string?)response.Result;
    }

    public async Task<string?> GetId()
    {
        await WaitForElements(By.ExistsSelector());

        var response = await Context.Browser.EvaluateScriptAsync($"{By.GetSelector()}.getAttribute('id')");
        return (string?)response.Result;
    }

}