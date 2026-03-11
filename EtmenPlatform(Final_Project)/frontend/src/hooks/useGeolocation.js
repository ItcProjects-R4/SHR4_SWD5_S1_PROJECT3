import { useState, useCallback } from "react";

/**
 * useGeolocation — Browser GPS hook.
 * Returns { location: {lat, lng}, error, loading, requestLocation }.
 * Triggered automatically for HIGH risk patients after 2-second delay.
 */
export function useGeolocation() {
  const [location, setLocation] = useState(null);
  const [error,    setError]    = useState(null);
  const [loading,  setLoading]  = useState(false);

  const requestLocation = useCallback(() => {
    if (!navigator.geolocation) {
      setError("Geolocation is not supported by this browser.");
      return;
    }
    setLoading(true);
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        setLocation({ lat: pos.coords.latitude, lng: pos.coords.longitude });
        setLoading(false);
      },
      (err) => { setError(err.message); setLoading(false); },
      { enableHighAccuracy: true, timeout: 10_000 }
    );
  }, []);

  return { location, error, loading, requestLocation };
}
