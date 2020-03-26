using UnityEngine;

namespace Editor.EditorHelpers
{
    public static class InspectorHelper
    {
        private static GUIStyle _labelNormalStyle;
        private static GUIStyle _labelErrStyle;

        public static GUIStyle LabelNormalStyle
        {
            get
            {
                var style = new GUIStyle("ControlLabel");
                return _labelNormalStyle ?? (_labelNormalStyle = style);
            }
        }

        public static GUIStyle LabelErrStyle
        {
            get
            {
                var style = new GUIStyle("ControlLabel") {normal = {textColor = Color.red}};
                return _labelErrStyle ?? (_labelErrStyle = style);
            }
        }
    }
}
