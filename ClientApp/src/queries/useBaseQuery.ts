import { useCallback, useContext, useEffect, useState } from "react";
import { UseQueryResult } from "react-query";
import { HubConnectionContext } from "../contexts";
import { ProjectionArgs } from "../api";

export function useQueryWithEvents<
  TResult,
  TResponse extends { version: number; result: TResult } = {
    version: number;
    result: TResult;
  }
>(
  query: UseQueryResult<TResponse, unknown>,
  argsType: ProjectionArgs["argsType"]
) {
  const { hubConnection } = useContext(HubConnectionContext);
  const [lastVersion, setLastVersion] = useState(-1);

  const {
    refetch,
    data: { version: currentVersion, result } = { version: 0 },
  } = query;

  const onProjectionUpdated = useCallback(
    (evt: ProjectionArgs) => {
      if (evt.argsType === argsType) setLastVersion(evt.version);
    },
    [argsType]
  );

  useEffect(() => {
    if (!hubConnection) return;
    hubConnection.on("projectionupdated", onProjectionUpdated);
    return () => hubConnection.off("projectionupdated", onProjectionUpdated);
  }, [hubConnection, onProjectionUpdated]);

  useEffect(() => {
    let timeout: NodeJS.Timeout;
    if (lastVersion > currentVersion) {
      timeout = setTimeout(refetch, 250);
    }
    return () => clearTimeout(timeout);
  }, [currentVersion, lastVersion, refetch]);

  const { data, ...others } = query;

  return { data: result, ...others };
}
