
import type { MatchPerformance } from "./MatchPerformance";
export interface Match{
  matchID: BigInt;          
  startDate: string; 
  cancelled: boolean;        
  matchPerformances: MatchPerformance[];
};