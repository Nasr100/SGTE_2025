// time-validators.ts
import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

export function arrivalBeforeDepartureValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const group = control as FormGroup;
    const arrivalTime = group.get('arrivalTime')?.value;
    const departureTime = group.get('departureTime')?.value;

    if (!arrivalTime || !departureTime) {
      return null; 
    }

    const arrivalDate = new Date(`1970-01-01T${arrivalTime}`);
    const departureDate = new Date(`1970-01-01T${departureTime}`);

    return arrivalDate >= departureDate 
      ? { timeSequence: true } 
      : null;
  };
}