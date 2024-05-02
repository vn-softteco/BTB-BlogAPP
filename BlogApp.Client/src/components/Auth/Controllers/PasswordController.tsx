import { Controller, Control, FieldPath } from 'react-hook-form'
import { TextField } from '@mui/material'
import { SxProps, Theme } from '@mui/system'

interface PasswordController<T extends object> {
  label: string
  placeholder: string
  control: Control<T>
  name: FieldPath<T>
  required?: boolean
  sx?: SxProps<Theme>
  small?: boolean
}

// TODO: hide password

const PasswordController = <T extends object>({
  label,
  placeholder,
  control,
  required = false,
  sx,
  name,
  small,
}: PasswordController<T>) => {

  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState: { error } }) => (
        <TextField
          fullWidth
          placeholder={placeholder}
          required={required}
          id={name}
          label={label}
          error={!!error}
          helperText={error && error.message}
          inputProps={{ 'aria-required': required }}
          sx={{ ...sx }}
          size={small ? 'small' : undefined}
          {...field}
        />
      )}
    />
  )
}

export default PasswordController
