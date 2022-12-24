using Gbs.Wasm.Store.ChurchUseCase.Actions;

namespace Gbs.Wasm.Store.ChurchUseCase;

public static class Reducers
{
    [ReducerMethod(typeof(FetchChurchesAction))]
    public static ChurchState ReduceFetchChurches(ChurchState state) =>
        new(true, state.Churches, null);
    
    [ReducerMethod]
    public static ChurchState ReduceFetchChurchesResultAction(ChurchState state, FetchChurchesResultAction action) =>
        new(false, action.Churches, action.Error);
    
    // [ReducerMethod]
    // public static ChurchState ReduceSetChurchesAction(ChurchState state, SetChurchesAction action) =>
    //     new ChurchState(churches: state.ClickCount + 1);
}