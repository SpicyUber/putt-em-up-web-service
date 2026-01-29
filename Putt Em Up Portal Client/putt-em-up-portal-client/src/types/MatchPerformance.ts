 
import type { ProfilePreview } from "./ProfilePreview";

export interface MatchPerformance {
  player: ProfilePreview;
  wonMatch: boolean;
  finalScore: number;
}
