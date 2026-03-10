import { useState, useCallback } from "react";

/**
 * Browser GPS hook for the Nearby Provider feature (v3.0).
 * Auto-requested with 2s delay for HIGH-risk patients on RiskPage.
 * Returns: { location: {lat, lng, accuracy} | null, error, loading, requestLocation }
 */
export function useGeolocation() {
  const [location, setLocation] = useState(null);
  const [error,    setError]    = useState(null);
  const [loading,  setLoading]  = useState(false);

  const requestLocation = useCallback(() => {
    if (!navigator.geolocation) {
      setError("الجهاز لا يدعم GPS");
      return;
    }
    setLoading(true);
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        setLocation({ lat: pos.coords.latitude, lng: pos.coords.longitude, accuracy: pos.coords.accuracy });
        setLoading(false);
      },
      (err) => {
        setError(err.message);
        setLoading(false);
      },
      { enableHighAccuracy: true, timeout: 10_000 }
    );
  }, []);

  return { location, error, loading, requestLocation };
}
