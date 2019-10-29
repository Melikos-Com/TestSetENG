/*
  HTTP 動作回傳物件
*/
declare interface JsonResult<T> {

    isSuccess: Boolean;

    message: string;

    totalCount: number;

    element: T;
}
