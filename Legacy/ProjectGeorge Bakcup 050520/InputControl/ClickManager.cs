using System;
using UnityEngine;

namespace ProjectGeorge.InputControl
{
    class ClickManager
    {
        private readonly int _button;

        // 4 separate functions to handel mouse actions

        private readonly Action _onMouseDown;
        private readonly Action _nonDragMouseRelease;
        private readonly Action _dragMouseRelease;
        private readonly Action _whileDraggingMouse;

        private Vector3 _lastMousePosition;
        private float _mouseDistanceTravelled = 0;

        private bool _primed = false;

        private bool isShiftMode;

        const float MOUSE_DRAG_THRESHOLD = 10.0f;

        public ClickManager(int button, Action onMouseDown, Action nonDragMouseRelease, Action dragMouseRelease, Action whileDraggingMouse, bool shiftMode = false)
        {
            if (onMouseDown == null || nonDragMouseRelease == null || dragMouseRelease == null || whileDraggingMouse == null)
                Debug.LogWarning("Missing callback while initializing ClickManager");

            _button = button;
            _onMouseDown = onMouseDown;
            _nonDragMouseRelease = nonDragMouseRelease;
            _dragMouseRelease = dragMouseRelease;
            _whileDraggingMouse = whileDraggingMouse;

            isShiftMode = shiftMode;
        }

        public void Update()
        {
            if (
                (!isShiftMode && !Input.GetKey(KeyCode.Space))
                || 
                (isShiftMode && Input.GetKey(KeyCode.Space))
                )
            {
                if (Input.GetMouseButtonDown(_button))
                {
                    // Mouse button down, start dragging
                    _primed = true;
                    _mouseDistanceTravelled = 0;
                    _lastMousePosition = Input.mousePosition;
                    _onMouseDown();
                }

                if (_primed && Input.GetMouseButton(_button) && !isDragClick())
                {
                    // Continue dragging
                    _mouseDistanceTravelled += Vector3.Distance(Input.mousePosition, _lastMousePosition);
                    _lastMousePosition = Input.mousePosition;
                }

                if (_primed && Input.GetMouseButton(_button) && isDragClick())
                    _whileDraggingMouse();

                if (_primed && Input.GetMouseButtonUp(_button))
                {
                    _primed = false;
                    if (isDragClick())
                        // Count as a drag complete
                        _dragMouseRelease();
                    else
                        // Count as a single click
                        _nonDragMouseRelease();
                }
            }
        }

        private bool isDragClick()
        {
            return _mouseDistanceTravelled > MOUSE_DRAG_THRESHOLD;
        }
    }
}