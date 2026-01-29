
import type { MatchPerformance } from "./MatchPerformance";
export interface Match{
  matchID: number;          
  startDate: string;         
  matchPerformances: MatchPerformance[];
};