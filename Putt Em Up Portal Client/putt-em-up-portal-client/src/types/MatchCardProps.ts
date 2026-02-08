import type { Match } from "./Match";

export interface MatchCardProps {
  match: Match;
  pid: BigInt; 
  viewerIsAdmin : boolean; 
  };