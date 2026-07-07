// Converts a datepicker Date value to a plain "yyyy-MM-dd" string for the API,
// built from local date parts (not toISOString(), which can shift the day
// across a timezone boundary).
export function toIsoDate(date: Date | null | undefined): string | undefined {
  if (!date) return undefined;
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}

// Parses an API date string (or undefined) into a Date for binding to a
// datepicker input.
export function fromIsoDate(value: string | null | undefined): Date | null {
  return value ? new Date(value) : null;
}
