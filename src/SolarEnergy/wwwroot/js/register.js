// Funcionalidades para a página de cadastro
document.addEventListener('DOMContentLoaded', function() {
    
    // Seleção de tipo de usuário
    const userTypeButtons = document.querySelectorAll('input[name="userTypeOptions"]');
    const userTypeHidden = document.getElementById('userTypeHidden');
    const cpfField = document.getElementById('cpfField');
    const cnpjField = document.getElementById('cnpjField');
    const cpfInput = document.getElementById('cpfInput');
    const cnpjInput = document.getElementById('cnpjInput');
    
    userTypeButtons.forEach(button => {
        button.addEventListener('change', function() {
            if (this.checked) {
                userTypeHidden.value = this.value;
                
                // Alternar entre CPF e CNPJ baseado no tipo de usuário
                if (this.value === '2') { // Empresa
                    cpfField.style.display = 'none';
                    cnpjField.style.display = 'block';
                    cpfInput.value = ''; // Limpar campo CPF
                    cpfInput.removeAttribute('required');
                    cnpjInput.setAttribute('required', 'required');
                    
                    // Mostrar dica para empresas
                    showTooltip(cnpjField, 'Para empresas, utilize o CNPJ da sua empresa.', 'info');
                } else { // Cliente
                    cpfField.style.display = 'block';
                    cnpjField.style.display = 'none';
                    cnpjInput.value = ''; // Limpar campo CNPJ
                    cnpjInput.removeAttribute('required');
                    cpfInput.setAttribute('required', 'required');
                    
                    // Mostrar dica para consumidores
                    showTooltip(cpfField, 'Para consumidores, utilize seu CPF pessoal.', 'info');
                }
            }
        });
    });
    
    // Função para mostrar tooltips
    function showTooltip(element, message, type = 'info') {
        // Remover tooltip anterior
        const existingTooltip = element.querySelector('.field-tooltip');
        if (existingTooltip) {
            existingTooltip.remove();
        }
        
        const tooltip = document.createElement('div');
        tooltip.className = `field-tooltip alert alert-${type} alert-dismissible fade show mt-1`;
        tooltip.style.padding = '0.25rem 0.5rem';
        tooltip.style.fontSize = '0.75rem';
        tooltip.innerHTML = `
            <i class="fas fa-info-circle me-1"></i>${message}
            <button type="button" class="btn-close btn-close-sm" data-bs-dismiss="alert"></button>
        `;
        
        element.appendChild(tooltip);
        
        // Auto-remove após 3 segundos
        setTimeout(() => {
            if (tooltip && tooltip.parentNode) {
                tooltip.remove();
            }
        }, 3000);
    }
    
    // Máscara para CPF
    if (cpfInput) {
        cpfInput.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length <= 11) {
                value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
                e.target.value = value;
            }
        });
    }
    
    // Máscara para CNPJ
    if (cnpjInput) {
        cnpjInput.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length <= 14) {
                value = value.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
                e.target.value = value;
            }
        });
    }
    
    // Máscara para telefone
    const phoneInput = document.getElementById('Phone');
    if (phoneInput) {
        phoneInput.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length <= 11) {
                if (value.length <= 10) {
                    value = value.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
                } else {
                    value = value.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
                }
                e.target.value = value;
            }
        });
    }
    
    // Validação de força da senha com mensagens em português
    const passwordInput = document.getElementById('Password');
    if (passwordInput) {
        passwordInput.addEventListener('input', function(e) {
            const password = e.target.value;
            let strength = 0;
            let suggestions = [];
            
            // Verificar critérios de força
            if (password.length >= 8) {
                strength++;
            } else {
                suggestions.push('pelo menos 8 caracteres');
            }
            
            if (/[a-z]/.test(password)) {
                strength++;
            } else {
                suggestions.push('uma letra minúscula');
            }
            
            if (/[A-Z]/.test(password)) {
                strength++;
            } else {
                suggestions.push('uma letra maiúscula');
            }
            
            if (/[0-9]/.test(password)) {
                strength++;
            } else {
                suggestions.push('um número');
            }
            
            if (/[^A-Za-z0-9]/.test(password)) {
                strength++;
            } else {
                suggestions.push('um caractere especial (!@#$%&*)');
            }
            
            // Remover indicador anterior se existir
            const existingIndicator = document.querySelector('.password-strength');
            if (existingIndicator) {
                existingIndicator.remove();
            }
            
            if (password.length > 0) {
                const indicator = document.createElement('div');
                indicator.className = 'password-strength mt-1';
                
                let strengthText = '';
                let strengthClass = '';
                let strengthIcon = '';
                
                switch (strength) {
                    case 0:
                    case 1:
                        strengthText = 'Muito fraca';
                        strengthClass = 'text-danger';
                        strengthIcon = 'fas fa-times-circle';
                        break;
                    case 2:
                        strengthText = 'Fraca';
                        strengthClass = 'text-warning';
                        strengthIcon = 'fas fa-exclamation-circle';
                        break;
                    case 3:
                        strengthText = 'Média';
                        strengthClass = 'text-info';
                        strengthIcon = 'fas fa-info-circle';
                        break;
                    case 4:
                        strengthText = 'Forte';
                        strengthClass = 'text-success';
                        strengthIcon = 'fas fa-check-circle';
                        break;
                    case 5:
                        strengthText = 'Muito forte';
                        strengthClass = 'text-success fw-bold';
                        strengthIcon = 'fas fa-shield-alt';
                        break;
                }
                
                let suggestionText = '';
                if (suggestions.length > 0) {
                    suggestionText = `<br><small class="text-muted">Adicione: ${suggestions.join(', ')}</small>`;
                }
                
                indicator.innerHTML = `<small class="${strengthClass}"><i class="${strengthIcon} me-1"></i>Força da senha: ${strengthText}${suggestionText}</small>`;
                passwordInput.parentNode.appendChild(indicator);
            }
        });
    }
    
    // Validação em tempo real para confirmação de senha
    const confirmPasswordInput = document.getElementById('ConfirmPassword');
    if (confirmPasswordInput && passwordInput) {
        confirmPasswordInput.addEventListener('input', function(e) {
            const password = passwordInput.value;
            const confirmPassword = e.target.value;
            
            // Remover indicador anterior
            const existingIndicator = document.querySelector('.password-match');
            if (existingIndicator) {
                existingIndicator.remove();
            }
            
            if (confirmPassword.length > 0) {
                const indicator = document.createElement('div');
                indicator.className = 'password-match mt-1';
                
                if (password === confirmPassword) {
                    indicator.innerHTML = '<small class="text-success"><i class="fas fa-check me-1"></i>Senhas conferem perfeitamente!</small>';
                } else {
                    indicator.innerHTML = '<small class="text-danger"><i class="fas fa-times me-1"></i>As senhas não coincidem</small>';
                }
                
                confirmPasswordInput.parentNode.appendChild(indicator);
            }
        });
    }
    
    // Animação de carregamento no submit
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', function(e) {
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton) {
                submitButton.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Criando sua conta...';
                submitButton.disabled = true;
                
                // Adicionar classe de loading
                submitButton.classList.add('btn-loading');
            }
        });
    }
    
    // Tooltip para campos obrigatórios com mensagens em português
    const requiredFields = document.querySelectorAll('input[required]');
    requiredFields.forEach(field => {
        field.addEventListener('blur', function() {
            if (!this.value.trim()) {
                this.classList.add('is-invalid');
                showFieldError(this, 'Este campo é obrigatório');
            } else {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
                hideFieldError(this);
            }
        });
        
        field.addEventListener('input', function() {
            if (this.classList.contains('is-invalid') && this.value.trim()) {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
                hideFieldError(this);
            }
        });
    });
    
    // Funções para mostrar/esconder erros de campo
    function showFieldError(field, message) {
        hideFieldError(field);
        const errorDiv = document.createElement('div');
        errorDiv.className = 'field-error invalid-feedback d-block';
        errorDiv.textContent = message;
        field.parentNode.appendChild(errorDiv);
    }
    
    function hideFieldError(field) {
        const existingError = field.parentNode.querySelector('.field-error');
        if (existingError) {
            existingError.remove();
        }
    }
    
    // Validação personalizada para CPF
    function validateCPF(cpf) {
        cpf = cpf.replace(/\D/g, '');
        if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) return false;
        
        let sum = 0;
        for (let i = 0; i < 9; i++) {
            sum += parseInt(cpf.charAt(i)) * (10 - i);
        }
        let remainder = sum % 11;
        let digit1 = remainder < 2 ? 0 : 11 - remainder;
        
        sum = 0;
        for (let i = 0; i < 10; i++) {
            sum += parseInt(cpf.charAt(i)) * (11 - i);
        }
        remainder = sum % 11;
        let digit2 = remainder < 2 ? 0 : 11 - remainder;
        
        return digit1 === parseInt(cpf.charAt(9)) && digit2 === parseInt(cpf.charAt(10));
    }
    
    // Validação personalizada para CNPJ
    function validateCNPJ(cnpj) {
        cnpj = cnpj.replace(/\D/g, '');
        if (cnpj.length !== 14 || /^(\d)\1{13}$/.test(cnpj)) return false;
        
        let weights1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        let weights2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        
        let sum = 0;
        for (let i = 0; i < 12; i++) {
            sum += parseInt(cnpj.charAt(i)) * weights1[i];
        }
        let digit1 = sum % 11 < 2 ? 0 : 11 - (sum % 11);
        
        sum = 0;
        for (let i = 0; i < 13; i++) {
            sum += parseInt(cnpj.charAt(i)) * weights2[i];
        }
        let digit2 = sum % 11 < 2 ? 0 : 11 - (sum % 11);
        
        return digit1 === parseInt(cnpj.charAt(12)) && digit2 === parseInt(cnpj.charAt(13));
    }
    
    // Adicionar validação nos campos com mensagens em português
    if (cpfInput) {
        cpfInput.addEventListener('blur', function() {
            if (cpfField.style.display !== 'none' && this.value) {
                if (!validateCPF(this.value)) {
                    this.setCustomValidity('CPF inválido. Verifique os números digitados.');
                    this.classList.add('is-invalid');
                    showFieldError(this, 'CPF inválido. Verifique os números digitados.');
                } else {
                    this.setCustomValidity('');
                    this.classList.remove('is-invalid');
                    this.classList.add('is-valid');
                    hideFieldError(this);
                }
            }
        });
    }
    
    if (cnpjInput) {
        cnpjInput.addEventListener('blur', function() {
            if (cnpjField.style.display !== 'none' && this.value) {
                if (!validateCNPJ(this.value)) {
                    this.setCustomValidity('CNPJ inválido. Verifique os números digitados.');
                    this.classList.add('is-invalid');
                    showFieldError(this, 'CNPJ inválido. Verifique os números digitados.');
                } else {
                    this.setCustomValidity('');
                    this.classList.remove('is-invalid');
                    this.classList.add('is-valid');
                    hideFieldError(this);
                }
            }
        });
    }
    
    // Validação de email em tempo real
    const emailInput = document.getElementById('Email');
    if (emailInput) {
        emailInput.addEventListener('blur', function() {
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (this.value && !emailRegex.test(this.value)) {
                this.classList.add('is-invalid');
                showFieldError(this, 'Por favor, insira um email válido (exemplo: seu@email.com)');
            } else if (this.value) {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
                hideFieldError(this);
            }
        });
    }
});