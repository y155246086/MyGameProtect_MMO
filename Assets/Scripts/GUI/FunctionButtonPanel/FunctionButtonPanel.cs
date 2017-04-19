using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FunctionButtonPanel : IViewBase {

    private Button fubenButton = null;
    private Button fanhuiButton = null;
    protected override void OnStart()
    {
        fubenButton = Find<Button>("FubenButton");
        fanhuiButton = Find<Button>("FanhuiButton");
    }
    protected override void AddEventListener()
    {
        fubenButton.onClick.AddListener(OnHubenHandler);
        fanhuiButton.onClick.AddListener(OnFanHuiHandler);
    }
    protected override void RemoveEventListener()
    {
        fubenButton.onClick.RemoveListener(OnHubenHandler);
        fanhuiButton.onClick.RemoveListener(OnFanHuiHandler);
    }
    private void OnFanHuiHandler()
    {
        GameStateManager.LoadScene(2);
    }

    private void OnHubenHandler()
    {
        GameStateManager.LoadScene(3);
    }

    protected override void OnShow(params object[] args)
    {
        fubenButton.gameObject.SetActive(args == null || args.Length == 0);
        fanhuiButton.gameObject.SetActive(!(args == null || args.Length == 0));
    }

    protected override void OnHide()
    {

    }
    protected override void OnDestory()
    {

    }
}
