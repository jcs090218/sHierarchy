#if UNITY_EDITOR
/**
 * Copyright (c) 2021 Jen-Chieh Shen
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software/algorithm and associated
 * documentation files (the "Software"), to use, copy, modify, merge or distribute copies of the Software, and to permit
 * persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * - The Software, substantial portions, or any modified version be kept free of charge and cannot be sold commercially.
 * 
 * - The above copyright and this permission notice shall be included in all copies, substantial portions or modified
 * versions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * For any other use, please ask for permission by contacting the author.
 */
using UnityEditor;
using UnityEngine;

namespace sHierarchy
{
    [System.Serializable]
    public class Data_Components : HierarchyComponent
    {
        /* Variables */

        private const string INFO = 
            @"";

        public bool foldout = false;

        public bool enabled = true;
        public bool focus = true;

        public float disableAlpa = 0.5f;

        /* Setter & Getters */

        /* Functions */

        public string FormKey(string name) { return HierarchyUtil.FormKey("components.") + name; }

        public void Init()
        {
            this.enabled = EditorPrefs.GetBool(FormKey("enabled"), true);
            this.focus = EditorPrefs.GetBool(FormKey("focus"), true);
            this.disableAlpa = EditorPrefs.GetFloat(FormKey("disableAlpa"), this.disableAlpa);
        }

        public void Draw()
        {
            foldout = EditorGUILayout.Foldout(foldout, "Components");

            if (!foldout)
                return;

            HierarchyUtil.CreateGroup(() =>
            {
                HierarchyUtil.CreateInfo(INFO);

                this.enabled = HierarchyUtil.Toggle("Enabeld", this.enabled,
                    @"Enable/Disable all features from this section");

                this.focus = HierarchyUtil.Toggle("Folding", this.focus, 
                    @"Focus the component after clicking the icon");

                HierarchyUtil.BeginHorizontal(() =>
                {
                    this.disableAlpa = HierarchyUtil.Slider("Disable Alpha", this.disableAlpa, 0.1f, 0.9f,
                        @"Alpha for disabled components");
                    HierarchyUtil.Button("Reset", ResetDisableAlpha);
                });
            });
        }

        public void SavePref()
        {
            EditorPrefs.SetBool(FormKey("enabled"), this.enabled);
            EditorPrefs.SetBool(FormKey("focus"), this.focus);
            EditorPrefs.SetFloat(FormKey("disableAlpa"), this.disableAlpa);
        }

        private void ResetDisableAlpha() { this.disableAlpa = 0.5f; }
    }
}
#endif
