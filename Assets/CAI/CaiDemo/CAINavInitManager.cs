/*
 * Copyright (c) 2011-2012 Stephen A. Pratt
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using UnityEngine;
using org.critterai.nav.u3d;
using org.critterai.nav;
using org.critterai.samples;

public class CAINavInitManager : MonoBehaviour
{

    private NavManager mManager;
    private NavGroup mNav;
    void Start()
    {

        NavManagerProvider provider = (NavManagerProvider)FindObjectOfType(typeof(NavManagerProvider));

        if (provider == null)
        {
            Debug.LogError(string.Format("{0}: There is no {1} in the scene."
                , name, typeof(NavManagerProvider).Name));

            enabled = false;
            return;
        }

        NavManager.ActiveManager = provider.CreateManager();  // Provides error reporting.

        if (NavManager.ActiveManager == null)
        {
            enabled = false;
            return;
        }

        mManager = NavManager.ActiveManager;
        mNav = mManager.NavGroup;

        SimGUIUtil.contextHelpText = string.Format(
            "Agent: Add: [{0}], Select: [{1}], Move Selected: [{2}]"
            , StdButtons.SetA, StdButtons.SelectB, StdButtons.SelectA);

        SimGUIUtil.contextControlZone.height = SimGUIUtil.LineHeight + SimGUIUtil.Margin;
        SimGUIUtil.contextActive = true;

    }

    void Update()
    {
        if (Time.frameCount % 30 == 0)
            System.GC.Collect();
        SimGUIUtil.Update();
    }

    void LateUpdate()
    {
        mManager.LateUpdate();
    }

    void OnDestroy()
    {
        NavManager.ActiveManager = null;
    }
}
