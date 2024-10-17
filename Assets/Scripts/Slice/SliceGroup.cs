using UnityEngine;

public class SliceGroup : MonoBehaviour
{
    public SliceHandler sliceHandler_0;
    public SliceHandler sliceHandler_1;
    public SliceHandler sliceHandler_2;

    enum State
    {
        SLICE_HANDLER_0, SLICE_HANDLER_1, SLICE_HANDLER_2
    }

    private State state;


    private void Start()
    {
        sliceHandler_0.Activate();
        sliceHandler_1.Deactivate();
        sliceHandler_2.Deactivate();

        state = State.SLICE_HANDLER_0;
    }

    //private void OnDisable()
    //{
    //    sliceHandler_0.Deactivate();
    //    sliceHandler_1.Deactivate();
    //    sliceHandler_2.Deactivate();

    //    state = State.SLICE_HANDLER_0;
    //}
    public void NextStage()
    {

    }

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
                    AudioControllerBlendera.Instance.Slice();

                    sliceHandler_0.Activate();
                    sliceHandler_1.Deactivate();
                    sliceHandler_2.Deactivate();

                    state = State.SLICE_HANDLER_0;

                    SliceFruit.Instance.StartAction();

                    SliceFruit.Instance.GoodSlice();
                }
                break;
            default:
                break;
        }
    }
}
