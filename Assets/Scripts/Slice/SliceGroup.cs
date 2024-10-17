using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SliceGroup : MonoBehaviour
{
    public SliceHandler sliceHandler_0;
    public SliceHandler sliceHandler_1;
    public SliceHandler sliceHandler_2;

    enum State
    {
        SLICE_HANDLER_0, SLICE_HANDLER_1, SLICE_HANDLER_2, NON_ACTIVE
    }

    private State state = State.NON_ACTIVE;

    private void Start()
    {
        sliceHandler_0.Activate();
        sliceHandler_1.Deactivate();
        sliceHandler_2.Deactivate();

        state = State.SLICE_HANDLER_0;
    }

    private void OnDisable()
    {
        sliceHandler_0.Activate();
        sliceHandler_1.Deactivate();
        sliceHandler_2.Deactivate();

        state = State.SLICE_HANDLER_0;
    }

    //private void Awake()
    //{
    //    sliceHandler_0.Deactivate();
    //    sliceHandler_1.Deactivate();
    //    sliceHandler_2.Deactivate();

    //    state = State.NON_ACTIVE;
    //}


    void Update()
    {
        switch (state)
        {
            case State.SLICE_HANDLER_0:
                if (!sliceHandler_0.IsActive())
                {
                    sliceHandler_1.Activate();
                    state = State.SLICE_HANDLER_1;
                    AudioControllerBlendera.Instance.Slice();
                }
                break;
            case State.SLICE_HANDLER_1:
                if (!sliceHandler_1.IsActive())
                {
                    sliceHandler_2.Activate();
                    state = State.SLICE_HANDLER_2;
                    AudioControllerBlendera.Instance.Slice();
                }
                break;
            case State.SLICE_HANDLER_2:
                if (!sliceHandler_2.IsActive())
                {
                    state = State.NON_ACTIVE;
                    AudioControllerBlendera.Instance.Slice();
                    
                    SliceFruit.Instance.GoodSlice();
                }
                break;
            case State.NON_ACTIVE:
                break;
            default:
                break;
        }
        if (!sliceHandler_0.IsActive())
        {
            return;
        }

        if (!sliceHandler_1.IsActive())
        {
            return;
        }

        if (!sliceHandler_2.IsActive())
        {
            return;
        }
    }
}
