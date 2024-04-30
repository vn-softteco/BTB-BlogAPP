import { Controller, Control, FieldPath } from 'react-hook-form'
import { TextField } from '@mui/material'
import { SxProps, Theme } from '@mui/system'

interface TextInputController<T extends object> {
    label: string
    placeholder: string
    name: FieldPath<T>
    control: Control<T>
    required?: boolean
    sx?: SxProps<Theme>
    small?: boolean
  }
  
  const TextInputController = <T extends object>({
    label,
    placeholder,
    control,
    required = false,
    sx,
    small,
    name
  }: TextInputController<T>) => {
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
            sx={{ borderRadius: '16px', ...sx }}
            inputProps={{ 'aria-required': required }}
            {...field}
            size={small ? 'small' : undefined}
          />
        )}
      />
    )
  }
  
  export default TextInputController